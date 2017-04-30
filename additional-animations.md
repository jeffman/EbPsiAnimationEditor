# Expanding the PSI Animation Table
This tutorial describes how to expand the PSI Animation Table in EarthBound.

In order to accomplish this, we need to relocate things to an area of the ROM with enough space to fit additional animation data. We also need to apply a short assembly hack and change a few configuration settings in the animation editor.

It goes without saying that we will be using an expanded ROM, since we need to relocate quite a bit of data. I'll also assume ROM addresses are for unheadered ROMs. (If you're using a headered ROM, add 0x200 to all ROM addresses.)

## 0. Overview
EarthBound has 34 PSI animations which can be called in-battle using the `[1C 13 00 XX]` control code ([reference](http://datacrystal.romhacking.net/wiki/EarthBound:Control_Codes#1C_-_Text)). The control code indexes them from 1 to 34 (0x22); the animation editor indexes them from 0 to 33.

If you click that link and go to the `[1C 13 00 XX]` section, you might notice that for numbers higher than 0x22, things other than PSI animations will happen. It goes up to 0x36, after which simply nothing will happen. So how do we add more animations?

Simply put, this tutorial will let you hack the game so that we can use numbers above 0x36 for additional PSI animations, and furthermore be able to edit them in the animation editor. So if we want to add one additional animation, it'll show up as number 34 in the editor and we'll access it in-game using `[1C 13 00 37]`; if we add a second additional animation, it'll be number 35 in the editor and accessed using `[1C 13 00 38]`; and so on.

## 1. Relocating Data
Three things need to be relocated:

1. Configuration table
2. Animation pointer table
3. Palette table

### Configuration table
The configuration table is originally stored at $CCF04D (0xCF04D in the ROM file). It has 34 entries with 12 bytes each, totalling 408 (0x198) bytes. For every animation you wish to add, you need to increase the size of this table by 12 bytes. So if you plan on adding one animation, you need to find an area of the ROM with at least 420 free bytes.

Under that assumption (that we're doubling the number of animations), let's find 816 (0x330) free bytes in the expanded area for this table. Even if you only plan on adding one animation, I still recommend allocating more space than necessary so that you don't have to relocate everything again in the future should you decide to change your mind.

I'm not using CCScript or CoilSnake or anything so I'll just find this using a hex editor. Let's try 0x320000 ($F20000). Using a hex editor, copy the 408 bytes at 0xCF04D to 0x320000. (This block begins with the hex sequence `27 DB 05 ..` and ends with `.. 4B FF 2F`.)

Note that there are (or should be) a bunch of zeroes following the data we just copied. The animation editor might barf when it sees this. So let's just go ahead and **paste those bytes one more time** after the first copy. This effectively adds another 34 dummy animation entries to the configuration table (we can edit the details of these entries later; this only serves to not make the editor crash/act strangely).

You should now have two copies of the configuration table stored from 0x320000 to 0x32032F (or wherever you chose to put it).

Write down the address where you're putting the table, because we'll need it later.

In summary, to relocate the configuration table:

1. Allocate `408 + (number of new animations * 12)` bytes somewhere in the expanded area of the ROM
2. Copy the old table to the new address
3. Copy dummy data into the new table entries so that the animation editor doesn't barf
4. Note down the new address of the table

## Animation pointer table
The animation pointer table is stored at $CCF58F (0xCF58F). It has 34 entries with 4 bytes each, totalling 136 (0x88) bytes. We're going to do _the exact same thing_ with this table that we did with the configuration table. In this example, we'll choose 0x320400 as the new address for the table. Go ahead and copy the 136 bytes at 0xCF58F to 0x320400, and then copy it again since we're duplicating the number of entries.

## Palette table
The palette table is stored at $CCF47F (0xCF47F). It has 34 entries with 8 bytes each, totalling 272 (0x110) bytes. Do the same thing again that we did with the other two tables. We'll choose 0x320600 as the new address for the palettes.

# 2. Assembly Hack
We still need to tell the game how to load these additional animations.

## Using CoilSnake
If you're using CoilSnake, create a new .ccs file in your project's `ccscript` folder and paste the following:

```
import asm65816

command ASMLoadAddress06(Address) {
	LDA_i	(short[0] Address)
	STA_d	(0x06)
	LDA_i	(short[1] Address)
	STA_d	(0x08)
}

ROM[0xC3F98D] = {
    JMP(0xFE00)
}

ROM[0xC3FE00] = {
    CMP_i(0xFFFF)  // 3
    BEQ(0x18)      // 2; L3  
    CMP_i(0x0023)  // 3
    BCC(0x0B)      // 2; L1
    CMP_i(0x0036)  // 3
    BCS(0x03)      // 2; L2
    JMP(0xF99B)    // 3

// L2:
    SBC_i(0x0014)  // 3
    STA_d(0x02)    // 2

// L1:
    LDA_d(0x02)    // 2
    JSL(0xC2E116)  // 4

// L3:
    JMP(0xFAC7)    // 3
}

ROM[0xC2E34F] = ASMLoadAddress06(0xF20000) // Configuration table
ROM[0xC2E154] = Int24(0xF20000)
ROM[0xC2E1BB] = Int24(0xF20000)
ROM[0xC2E50A] = Int24(0xF20000)
ROM[0xC2E461] = ASMLoadAddress06(0xF20400) // Animation pointer table
ROM[0xC2E2F4] = ASMLoadAddress06(0xF20600) // Palette table
```

Those last three lines contain the new table addresses. They need to be in SNES addressing, which basically means add 0xC00000 to the ROM address (??? how does this differ for ExHiRom someone help ???) Obviously, if you used different addresses for the new tables from what I've used, you should update them accordingly in those last three lines.

## Not using CoilSnake
If you're not using CoilSnake, or for whatever reason don't want to use CCScript, follow these steps to apply the hack with a hex editor. All pastes are overwrites, not inserts. Keep in mind that I'm using unheadered ROM addresses (so add 0x200 to all ROM addresses if you're using a headered ROM).

1. Open your hex editor and go to 0x3FE00, and paste the following 32 bytes:
`C9 FF FF F0 18 C9 23 00 90 0B C9 36 00 B0 03 4C 9B F9 E9 14 00 85 02 A5 02 22 16 E1 C2 4C C7 FA`
2. Go to 0x3F98E and paste the following 2 bytes: `00 FE`
3. Recall the address of the new configuration table (0x320000 in our example). In your mind, convert it to a SNES address and split the result into four bytes: 00 F2 00 00. Call these four bytes Alice, Bob, Chris, and MrTenda. Go to 0x2E34F and paste the following 10 bytes: `A9 [MrTenda] [Chris] 85 06 A9 [Bob] [Alice] 85 08`. In our example, we would paste `A9 00 00 85 06 A9 F2 00 85 08`.
4. Go to 0x2E461 and _repeat step 3 **using the new pointer table address**_ (0x320400 in our example).
5. Go to 0x2E2F4 and _repeat step 3 **using the new palette table address**_ (0x320600 in our example).
6. Go to 0x2E154 and paste: `[MrTenda] [Chris] [Bob]`, but _not_ `[Alice]`. Only three bytes here. In our example, we would paste `[00 00 F2]`.
7. Go to 0x2E1BB and _repeat step 6_.
8. Go to 0x2E50A and _repeat step 6_. Yes, it's the configuration table address repeated for steps 7 and 8.

# 3. PSI Animation Editor
With the hard stuff out of the way, we just need to update some settings in the .romconfig file. Go to your ROM folder and open the .romconfig file in a text editor. (If it's not there, open the ROM in the PSI animation editor and it'll create it for you. Then close the editor.) In the Parameters section, change:

1. `psi animation info`: new configuration table ROM address, in decimal (3276800 in our example)
2. `psi animation count`: 68, since we doubled the number of animations
3. `psi animation arrangements`: decimal ROM address of the arrangement pointer table (3277824 in our example)
4. `psi animation palettes`: decimal ROM address of the palette table (3278336 in our example)

We also need to allocate some free space for new animation data that gets saved in the animation editor. We'll use 64 kB at 0x321000 (decimal 3280896) in this example. In the FreeRanges section, add:

```
{
  "Start": 3280896,
  "Length": 65536
}
```

Finally, the amount of animation names needs to match the animation count, so go ahead and duplicate that whole list under AnimationNames. Save and open your ROM in the animation editor. You should now be able to save new animations!

# 4. CoilSnake
If you're using CoilSnake, you'll want to tell it not to use up the free ranges we just allocated. We also need to tell it not to use the small block at 0x3FE00 since we're using it for hack code. In your project folder, open up used_ranges.yml and add:

```
- (0x3FE00, 0x40000)
- (0x320000, 0x331000)
```

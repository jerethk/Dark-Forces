This is a BM converter for Windows, which requires .NET to run.
BM files contain Dark Forces textures.

The program has the following functions:
- View Dark Forces BM files
- Convert a BM file to PNG file(s)
- Bulk conversion (multiple textures) from BM to PNG
- Create a new BM file from source images (BMP, PNG or JPEG)
- Create transparent, weapon, or multiple (switch or animated) BM files


VIEW AND CONVERT BMs
--------------------
The BM viewing window is self-explanatory.
You may load a PAL file; otherwise, SECBASE.PAL is used by default.
Click the "Export PNG" button to convert the currently opened BM into a PNG image(s).

"Bulk convert" allows you to select multiple BM files and convert them all into PNGs. (It will only convert single BMs; multi BMs will be skipped over.)


CREATE NEW BM
-------------
You may load a PAL file; otherwise, SECBASE.PAL is used by default.
Choose which kind of BM you want to create: Single or Multi

Choose the transparency: opaque, transparent or weapon.
If transparent or weapon are selected, colour 0 (black) will be treated as transparent by the DF engine.

Add an image (or images) from a PNG, BMP or JPG file. Note: Unless you are creating a weapon BM, the image width and height must be powers of 2.

If creating a multi BM, enter the frame rate. A frame rate of "0" will be a switch texture. Otherwise, it will be an animated texture.

Select whether you want compression to be used. (For single BMs only.) If creating a non-transparent BM, compression format 1 (RLE) will be used. If creating a transparent BM, compression format 2 (RLE0) will be used.

When the BM is created, colours from your source image(s) will be matched to the closest available colour in the PAL which you are using. If you have checked "include illuminated colours", colour matching will include PAL indexes 1-31, which are the "glow-in-the-dark" colours. These colours appear bright under any light level.


This program was written by Jereth K.
# AAI-Texture-Redrawer
The tool for automatical redrawing of some types of sprites (Texture2D) of AAI mobile port.

# How to use this tool?
First of all, you need to get executable file whether via building the project in VS or using the already built one inside the debug/bin folder.<br>
Start the executable, open settings, choose directories that will contain certain type of textures and press Apply button.<br>
And then you can choose the type that you want to edit in listbox, press Open and the program will open the sprites of this type in directory that you've chosen in settings. One warning, though. In order for it to work correctly, all the sprites should have their original names.<br>
You will be able to write some text in text box and then transform it into an image by pressing Show button. If the image is of your liking, you can save it by pressing Save Button.<br>
But if you want the program to redraw every single sprite automatically you'll need to press the Save With Text File button. It will open a dialog of choosing the file with all the new texts of sprites that you want edit. But remember that text file should have new text FOR EVERY SINGLE SPRITE and for every single sprite there should be from 1 to 3 lines depending on which type of sprite you redraw. While item names have only one row all the times, the small keyword names should contain two rows (you can make second one empty).

# Fonts
If you don't have Liter, MT Pro Cyrillic and all the variations of Open-Sans fonts installed on your device, you can get them in Fonts folder and install them.

# Supported Types
1. item_n (Item Name)
2. item_d (Item Description on their right)
3. item_t (Item Text at the bottom)
4. Keyword_l (Big Keyword Names)
5. Keyword_s (Small Keyword Names used on Logic buttons)
6. Keyword_t (Keyword Text)
7. Win_Name (Nameplates of dialog window)
8. select_l (Answer options)
9. select_s (Topic options)

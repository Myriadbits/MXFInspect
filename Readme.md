# MXF Inspect

MXF Inspect is a **fully functional and completely free** Windows tool to display the internal structure of a MXF (Material eXchange Format) file. It can NOT play the MXF movie itself. There is no demo and advanced mode, it is completely free. The application is tested on Windows 7, 8 and 10.

MXF files are extensively used in the broadcast industry. Since I was working in the broadcast industries, I personally used a lot of MXF files. I wanted to determine if certain MXF files were valid but could not find any good (free) tools on the internet so I decided to make my own. The source code is released under the GPL.

## Features
* Open multiple MXF files at once.
* View offsets, parsed MXF data and raw data in a glance.
* Offset and Logical view present.
* Jump to the next/previous object of the same type.
* Only show/filter the current object type.
* Show/hide fillers.
* Large file support. It is possible to load very huge MXF files (several Gigabyte) in seconds.
* 'Syntax‘ coloring. Different types of metadata can be given separate colors so it is easier to distinguish between the different types.
* Report screen that shows the results of the following tests:
	* Consistency of partitions (check if the previous/next partitions and footer partitions are filled in correctly)
	* RIP check (present and pointing to valid partitions)
	* Check if every entry in all index tables point to valid essences.
	* Test if the user dates in all system items (if present) increase correct (no jumps present).
	* Test continuity counter of the system items increases.

**This application does NOT implement the WHOLE SMPTE-MXF specifications. This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.** 

## SMPTE
The MXF file specification is huge! The specification allows for a lot of different ‘tastes’ of MXF files. I started developing this application by using the SMPTE 377-1-2009 specifications only. But during development I realized that I needed to use a lot of other SMPTE specs as well. Some specifications are a bit unclear (at least to me). I don’t know if I understood/implemented them correctly which might result in a warning or error for some tests… The default label and key names found in SMPTE 377 are hardcoded into the MXF library, all other names are dynamically retrieved from the following SMPTE lists:

* SMPTE Metadata Dictionary as specified in RP210v13-2012
* SMPTE Labels Registry as specified in RP224v12-2012
* SMPTE Class 13 and Class 14 registrations list


## Screenshots

Some screenshots:

Report screen showing the new ‘Execute all tests’ button

![Report screen showing the new ‘Execute all tests’ button](https://www.myriadbits.com/wp-content/uploads/2015/01/Report1.png)

Logical view

![Logical view](https://www.myriadbits.com/wp-content/uploads/2015/01/Logical.png)

MXF file tree (with 'syntax' coloring)

![MXF file tree (with 'syntax' coloring)](https://www.myriadbits.com/wp-content/uploads/2015/01/WholeFile2.png)


## Future work
There are a number of features that require some work:

* Implement more tests in the report screen
* Improve the tracks information display in the report screen.
* Logical view should also include the logical track object hierarchy, maybe include the data itself.
* Ability to display the real subtitles information.
* Search and jump to a specific date/time/frame number.
* Search for text.


## Installation

Windows setup file of the 2.2.0.4 version can be downloaded on my website: [MXF Inspect](https://www.myriadbits.com/index.php/mxf-inspect-2/#)


Have fun!

*Jochem*

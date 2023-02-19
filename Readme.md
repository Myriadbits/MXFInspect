[![License: MIT](https://img.shields.io/badge/License-LGPL-blue.svg)](https://www.gnu.org/licenses/lgpl-3.0.html)

# MXF Inspect

MXF Inspect is a **fully functional and completely free** Windows tool to display the internal structure of a MXF (Material eXchange Format) file. It can NOT play the MXF movie itself. The application is tested on Windows 7, 8, 10 & 11.

MXF files are extensively used in the broadcast industry. Since I was working in the broadcast industries, I personally used a lot of MXF files. I wanted to determine if certain MXF files were valid but could not find any good (free) tools on the internet so I decided to make my own. The source code is released under the LGPL.

This project is updated in februari 2023 by merging the Rayden84 fork. This project now includes all changes that have been done in the Rayden84 fork (https://github.com/rayden84). It includes code of the following contributors: feigenanton, rayden84, ft, ws, Nicolas Gaullier and Wolfgang Ruppel. They have made numerous updates and expanded the original project far beyond my original ideas. The original code has been totally revamped, clean-up, restructured, great work guys! 

## MXF Inspect Features
* Open multiple MXF files at once.
* Support for different MXF File Version (e.g. 1.2, 1.3)
* Support for large number of MXF objects, see this [TODO list](/tree.md) for the classes that are supported. 
* View offsets, parsed MXF data and raw data in a glance.
* Offset and Logical view present.
* Jump to the next/previous object of the same type.
* Only show/filter the current object type.
* Written in .NET6. Thus allowing MXFInspect to be shipped as a single-file-application without any dependencies on installed .NET frameworks
* Large file support. It is possible to load very huge MXF files (several Gigabytes).
* 'Syntax‘ coloring. Different types of metadata can be given separate colors so it is easier to distinguish between the different types.
* Drag and drop of MXF file in order to open it
* Report screen that shows the results of the following tests:
	* Consistency of partitions (check if the previous/next partitions and footer partitions are filled in correctly)
	* RIP check (present and pointing to valid partitions)
	* Check if every entry in all index tables point to valid essences.
	* Test if the user dates in all system items (if present) increase correct (no jumps present).
	* Test continuity counter of the system items increases.

**This application does NOT (yet) implement the WHOLE SMPTE-MXF specifications. This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the Lesser GNU General Public License for more details.** 

## Screenshots

Some screenshots:

Report screen showing the new ‘Execute all tests’ button

![Report screen showing the new ‘Execute all tests’ button](doc/screenshots/Report.png)

Logical view

![Logical view](doc/screenshots/Logical.png)

MXF file tree (with 'syntax' coloring)

![MXF file tree (with 'syntax' coloring)](doc/screenshots/WholeFile2.png)


**Latest improvements:**

- Further SMPTE Standards Implementation
  - Addition of new metadata classes (e.g. MPEGPictureEssenceDescriptor, JPEG2000PictureSubDescriptor). A hierarchical inheritance tree of MXF classes can be found [here](https://registry.smpte-ra.org/view/published/Groups_inheritance_tree.html). See this [TODO list](/tree.md) for the classes that still have to be implemented. 
  - New MXF types (e.g. CodedContentScanningKind, ProductVersion, FrameLayout, Emphasis). A list of MXF types can be found [here](https://registry.smpte-ra.org/view/published/ul_hierarchy.html?rgr=t)

## Future work

However, there is still a long **TODO list:**
- implement additional/more thorough file checks/validations such as:
  - Is KAG Size consistent?
  - Is Edit Unit Size constant?
- implementation of validators, that check whether a MXF file conforms to the [ARD ZDF profiles](https://www.irt.de/en/publications/technical-guidelines/technical-guidelines-download/mxf)
- true MDI application, where multiple MXF files can be opened side-by-side
- pimp up the property grid (colors, hyperlinks for UMIDs)
- use a better control for the hex view 
- migrate code from using backgroundWorker to async/await
  - make file loading abortable (cancellable)
- optimize the MXFReader class to reduce file loading time
- reorganize the file check dialog
  - investigate on making tests hierarchical
  - add general file info dialog
  - add report (in xml) for conformance check
  - add checksum to file

**Any help/contribution is greatly appreciated!!!**

## Installation

Find the latest release and just download the self-contained single file for your Windows version, copy the zip contents to a separate folder and run the MXFInspect.exe.

**Have fun!**

<br>
<br>

*PS: The MXF file specification is huge! The specification allows for a lot of different ‘tastes’ of MXF files. I started developing this application by using the SMPTE 377-1-2009 specifications only. But during development I realized that I needed to use a lot of other SMPTE specs as well. Some specifications are a bit unclear (at least to me). I don’t know if I understood/implemented them correctly which might result in a possible incorrect warning or error for some tests…*


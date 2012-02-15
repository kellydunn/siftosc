```
             ___  __
       __  /'___\/\ \__
  ____/\_\/\ \__/\ \ ,_\   ___     ____    ___
 /',__\/\ \ \ ,__\\ \ \/  / __`\  /',__\  /'___\
/\__, `\ \ \ \ \_/ \ \ \_/\ \L\ \/\__, `\/\ \__/
\/\____/\ \_\ \_\   \ \__\ \____/\/\____/\ \____\
 \/___/  \/_/\/_/    \/__/\/___/  \/___/  \/____/
```
# what
sf music hack day 2012 project.  Allows Sifteo Cubes to send OSC messages to various OSC servers, adding an additional dimension to your musical expression!

# installation
Requires the following:

- mono
- xbuild
- Sifteo SDK + Siftdev (http://developer.sifteo.com)

Dependencies are kept in the `lib` folder, as I'm still learning how C# handles deps management :P

```
cd siftosc
xbuild
mono Siftosc/bin/Debug/Game.exe
```

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
# usage
The end goal is to configure how your sifteo cubes will talk to an OSC server via the config.yaml file found at the root directory of the project.  Ideally, you will have full control over what cube callbacks get associated with which OSC servers, and what messages they send via this configuration file.

A sample config.yaml file would look something like this:

```
1:
  TiltEvent:
    "127.0.0.1:9001" :
      - /renoise/tilt[iii]
```

Which would delegate the `TiltEvent` of your first cube to send the OSC message `/renoise/tilt` with the three variables exposed to you via the Sifteo SDK TiltEvent callbacks `x, y, z` to the OSC server residing at `127.0.0.1:9001`

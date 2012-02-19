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
SF Music Hack Day 2012 project.  Allows Sifteo Cubes to send OSC messages to various OSC servers.

# installation
Requires the following:

- mono
- xbuild
- Sifteo SDK + Siftdev (http://developer.sifteo.com)

Third Party dependencies are kept in the `lib` folder, as I'm still learning how C# handles deps management :P

# build

```
cd siftosc
xbuild
mono Siftosc/bin/Debug/Game.exe
```
# usage
SiftOsc is completely controlled via the `config.yml` file located in the root directory of the application.  Simply specify Cube Callback events with corresponding OSC Server endpoints and the desired messages to send to them for the desired effect.

# examples

A sample `config.yml` file would look something like this:

```
0:
  TiltEvent:
    "127.0.0.1:9001" :
      - /renoise/tilt
```

Which would delegate the `TiltEvent` of your first cube to send the OSC message `/renoise/tilt` with the three variables exposed to you via the Sifteo SDK TiltEvent callbacks `x, y, z` to the OSC server residing at `127.0.0.1:9001`

Consider the following example:

```
0:
  TiltEvent:
    "127.0.0.1:9001" :
      - /renoise/tilt
      - /monome/tilt
      - /kaoscillator/tilt
1:
  ShakeStartedEvent:
    "110.10.10.10" :
      - /bloopsaphone/shake
    "110.10.10.11" :
      - /maracas/shake
```

Here, you can see that it's possible to attach the `TiltEvent` of one cube to many different messages that get sent to the same endpoint `127.0.0.1:9001`.  Which means you can send many messages to one OSC server per cube event.  Additionally, you can send messages to different endpoints for each cube event, as denoted with the `ShakeStartedEvent` configuration listed for cube #1.

Happy hacking (╮^—^)╯ <3<3<3

Kelly

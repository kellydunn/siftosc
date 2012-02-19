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


# exampls

A sample config.yaml file would look something like this:

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
  - Kelly

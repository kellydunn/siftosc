using Sifteo;
using System;
using Bespoke.Common;
using Bespoke.Common.Osc;

namespace SiftOsc {
  public class SiftOsc : BaseApp {

    override public int FrameRate {
      get { return 20; }
    }

    // called during intitialization, before the game has started to run
    override public void Setup() {
      Log.Debug("Setup()");
    }

    override public void Tick() {
      Log.Debug("Tick()");
    }

    // development mode only
    // start SiftOsc as an executable and run it, waiting for Siftrunner to connect
    static void Main(string[] args) {
      new SiftOsc().Run();
    }
  }
}


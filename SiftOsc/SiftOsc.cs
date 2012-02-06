using Sifteo;
using System;
using System.Net;
using System.IO;
using Bespoke.Common;
using Bespoke.Common.Osc;

namespace SiftOsc {
  public class SiftOsc : BaseApp {
    private OscServer server;
    private OscClient client;
    private IPAddress address;
    private int port;

    override public int FrameRate {
      get { return 20; }
    }

    override public void Setup() {
      Log.Debug("Setup()");
      server = new OscServer(TransportType.Udp, IPAddress.Loopback, 3333);
      address = IPAddress.Parse("127.0.0.1");
      port = 7123;
      client = new OscClient(address, port);
    }

    override public void Tick() {
      Log.Debug("Tick()");
    }

    static void Main(string[] args) {
      new SiftOsc().Run();
    }
  }
}


using Sifteo;
using System;
using System.Net;
using System.IO;
using System.Drawing;
using Bespoke.Common;
using Bespoke.Common.Osc;

namespace SiftOsc {
  public class SiftOsc : BaseApp {
    private OscServer server;
    private OscClient client;
    private IPEndPoint endPoint;
    private int port;

    override public int FrameRate {
      get { return 20; }
    }

    override public void Setup() {
      server = new OscServer(TransportType.Udp, IPAddress.Loopback, 3333);
      server.RegisterMethod("siftosc/tilt");
      server.MessageReceived += new OscMessageReceivedHandler(MessageReceived);
      server.Start();

      client = new OscClient(IPAddress.Loopback, 7001);
      endPoint = new IPEndPoint(IPAddress.Loopback, 3333);

      foreach(var cube in this.CubeSet) {
        cube.TiltEvent += OnTilt;
      }
    }

    public void OnTilt(Cube c, int x, int y, int z) {
      Log.Debug("x: " + x + " y: " + y + " z:" + z);
      OscMessage oscMessage = new OscMessage(endPoint, "/siftosc/tilt", client);
      OscBundle bundle = new OscBundle(endPoint, client);
      bundle.Append(oscMessage);
      bundle.Send(endPoint);
    }

    static void MessageReceived(object sender, OscMessageReceivedEventArgs e) {
      Log.Debug(string.Format("Message Received [{0}]: {1}", e.Message.SourceEndPoint.Address, e.Message.Address));
    }

    override public void Tick() { }

    static void Main(string[] args) {
      new SiftOsc().Run();
    }
  }
}


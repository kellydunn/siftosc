using Sifteo;
using System;
using System.Net;
using System.IO;
using System.Drawing;
using Bespoke.Common;
using Bespoke.Common.Osc;
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
      server.BundleReceived += new OscBundleReceivedHandler(BundleReceived);
      server.Start();

      client = new OscClient(IPAddress.Loopback, 7001);
      endPoint = new IPEndPoint(IPAddress.Loopback, 9001);

      foreach(var cube in this.CubeSet) {
        cube.TiltEvent += OnTilt;
      }
    }

    public void OnTilt(Cube c, int x, int y, int z) {
      OscMessage oscMessage = new OscMessage(endPoint, "/renoise/song/bpm", client);
      oscMessage.Append("" + (x * 100));
      OscMessage oscMessage2 = new OscMessage(endPoint, "/renoise/song/edit/octave", client);
      oscMessage2.Append("" + (y + 2));
      OscMessage oscMessage3 = new OscMessage(endPoint, "/renoise/song/edit/step", client);
      oscMessage3.Append("" + (z + 2));
      //OscBundle bundle = new OscBundle(endPoint, client);
      //bundle.Append(oscMessage);
      //bundle.Append(oscMessage2);
      //bundle.Send(endPoint);
      oscMessage.Send(endPoint);
      oscMessage2.Send(endPoint);
      oscMessage3.Send(endPoint);
    }

    static void BundleReceived(object sender, OscBundleReceivedEventArgs e) {
      Log.Debug(string.Format("Message Received [{0}]: {1}", e.Bundle.SourceEndPoint.Address, e.Bundle.Address));
    }

    override public void Tick() { }

    static void Main(string[] args) {
      new SiftOsc().Run();
    }
  }
}


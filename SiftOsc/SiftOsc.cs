using Sifteo;
using System;
using System.Net;
using System.IO;
using System.Drawing;
using Bespoke.Common;
using Bespoke.Common.Osc;
using Yaml;

namespace SiftOsc {
  public class SiftOsc : BaseApp {
    private OscClient client;
    private IPEndPoint renoiseEndPoint;
    private IPEndPoint endPoint;
    private int port;

    override public int FrameRate {
      get { return 20; }
    }

    override public void Setup() {
      client = new OscClient(IPAddress.Loopback, 7001);

      IPAddress ipAddress;
      bool demoing = true;
      if(demoing) {
        ipAddress = IPAddress.Parse("10.10.10.199");
      } else {
        ipAddress = IPAddress.Loopback;
      }

      renoiseEndPoint = new IPEndPoint(ipAddress, 9001);
      endPoint = new IPEndPoint(ipAddress, 9002);

      this.CubeSet[0].TiltEvent += OnTilt;

      this.CubeSet[1].ShakeStartedEvent += OnShakeStarted;

      this.CubeSet[2].ShakeStoppedEvent += OnShakeStopped;
      this.CubeSet[2].Image("../assets/dumb.jpg");
    }

    public void OnShakeStarted(Cube c) {
      OscMessage oscMessage = new OscMessage(endPoint, "/renoise/transport/start");
      Log.Debug("" + endPoint.Address);
      oscMessage.Send(renoiseEndPoint);
    }

    public void OnShakeStopped(Cube C, int duration) {
      if(duration > 75) {
        OscMessage oscMessage = new OscMessage(endPoint, "/renoise/transport/start");
        Log.Debug("" + endPoint.Address);
        oscMessage.Send(renoiseEndPoint);

        // Potentially the worst hack of all time
        OscMessage oscMessage2 = new OscMessage(endPoint, "/siftosc/drop", client);
        oscMessage2.Send(endPoint);
      }
    }

    public void OnTilt(Cube c, int x, int y, int z) {
      OscMessage oscMessage = new OscMessage(endPoint, "/siftosc/tilt", client);
      oscMessage.Append(x);
      oscMessage.Append(y);
      oscMessage.Append(z);
      Log.Debug("x: " + x + "y: " + y + "z: " + z);
      oscMessage.Send(endPoint);
    }

    static void Main(string[] args) {
      new SiftOsc().Run();
    }
  }
}


using Sifteo;
using System;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Bespoke.Common;
using Bespoke.Common.Osc;
using YamlDotNet.Core;
using YamlDotNet.Converters;
using YamlDotNet.RepresentationModel;

namespace SiftOsc {
  public class SiftOscCube {
    private Cube cube;
    private OscClient client;
    private Dictionary<String, List<SiftOscCubeEvent>> siftOscCubeEvents;

    public SiftOscCube(Cube cube, OscClient client, Dictionary<String, List<SiftOscCubeEvent>> siftOscCubeEvents) {
      this.cube = cube;
      this.client = client;
      this.siftOscCubeEvents = (siftOscCubeEvents != null)? siftOscCubeEvents : new Dictionary<String, List<SiftOscCubeEvent>>();
    }

    public void setCube(Cube c) {
      this.cube = c;
    }

    public void generateFromYaml(YamlMappingNode cubeEventsNode) {
      foreach (var cubeEvent in cubeEventsNode.Children) {
        String cubeEventName = (((YamlScalarNode)cubeEvent.Key).Value);

        YamlMappingNode eventEndpoints = (YamlMappingNode)cubeEvent.Value;
        List<SiftOscCubeEvent> cubeEvents = new List<SiftOscCubeEvent>();

        foreach (var eventEndpoint in eventEndpoints.Children) {
          SiftOscCubeEvent siftOscCubeEvent = new SiftOscCubeEvent(cube, this.client, null, null);
          siftOscCubeEvent.generateFromYaml(eventEndpoint);
          cubeEvents.Add(siftOscCubeEvent);
        }

        this.siftOscCubeEvents.Add(cubeEventName, cubeEvents);
      }
    }

    public void attachEvents() {
      foreach(KeyValuePair<String, List<SiftOscCubeEvent>> siftOscCubeEvent in this.siftOscCubeEvents) {
        foreach(SiftOscCubeEvent cubeEvent in siftOscCubeEvent.Value) {
          foreach(SiftOscEventMessage eventMessage in cubeEvent.getMessages()) {
            switch(siftOscCubeEvent.Key) {
              case "ButtonEvent" :
                this.cube.ButtonEvent += eventMessage.OnButton;
                break;
              case "TiltEvent" :
                this.cube.TiltEvent += eventMessage.OnTilt;
                break;
              case "ShakeStartedEvent" :
                this.cube.ShakeStartedEvent += eventMessage.OnShakeStarted;
                break;
              case "ShakeStoppedEvent" :
                this.cube.ShakeStoppedEvent += eventMessage.OnShakeStopped;
                break;
              case "FlipEvent" :
                this.cube.FlipEvent += eventMessage.OnFlip;
                break;
              case "NeighborAddEvent" :
                this.cube.NeighborAddEvent += eventMessage.OnNeighborAdd;
                break;
              case "NeighborRemoveEvent" :
                this.cube.NeighborRemoveEvent += eventMessage.OnNeighborRemove;
                break;
            }
          }
        }
      }
    }

  }
}
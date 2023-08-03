// Copyright (C) 2023 James Coliz, Jr. <jcoliz@outlook.com> All rights reserved
// Use of this source code is governed by the MIT license (see LICENSE file)

namespace BrewHub.Protocol.Mqtt;  
   
public class MqttOptions
{
    public static string Section => "MQTT";

    public string? Server { get; set; }

    public int Port { get; set; } = 1883;

    public string Topic { get; set; } = "brewhub;1";

    public string Site { get; set; } = "none";

    public string? ClientId { get; set; }
}

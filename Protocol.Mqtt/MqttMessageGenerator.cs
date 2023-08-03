// Copyright (C) 2023 James Coliz, Jr. <jcoliz@outlook.com> All rights reserved
// Use of this source code is governed by the MIT license (see LICENSE file)

namespace BrewHub.Protocol.Mqtt;

public class MessageGenerator
{
    public enum MessageKind { Invalid = 0, Data, Telemetry, ReportedProperties, DesiredProperties, Command, NodeCommandAll };
    private readonly MqttOptions? _options;
    public MessageGenerator(MqttOptions? options)
    {
        _options = options;
    }

    public (string topic, MessagePayload payload) Generate(MessageKind kind, string? deviceid, string? componentid, string model, Dictionary<string, object> metrics)
    {
        // Get the topic
        var topic = GetTopic(kind, deviceid, componentid);

        // Assemble the message
        var payload = new MessagePayload()
        { 
            Model = model,
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            Metrics = metrics
        };

        return (topic, payload);
    }

    public string GetTopic(MessageKind kind, string? deviceid = null, string? componentid = null)
    {
        // Make sure we were configured with options
        if (_options is null)
            throw new ApplicationException("No MQTT options configured");

        var iscomponent = !string.IsNullOrEmpty(componentid);
        var mtype = (kind, iscomponent) switch
        {
            (MessageKind.Data, true) or
            (MessageKind.Telemetry, true) => "DDATA",
            
            (MessageKind.Data, false) or
            (MessageKind.ReportedProperties, false) => "NDATA",
            
            (MessageKind.Command, _) or
            (MessageKind.DesiredProperties,_) or
            (MessageKind.NodeCommandAll,_) => "NCMD",

            _ => throw new NotImplementedException($"Message Kind {kind} is not implemented.")
        };
        var topicdeviceid = deviceid ?? _options!.ClientId;

        var topic = $"{_options.Topic}/{_options.Site}/{mtype}/{topicdeviceid}" + (iscomponent ? $"/{componentid}" : string.Empty) + (kind == MessageKind.NodeCommandAll ? "/#" : string.Empty);

        return topic;
   }
}
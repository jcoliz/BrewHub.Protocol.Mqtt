// Copyright (C) 2023 James Coliz, Jr. <jcoliz@outlook.com> All rights reserved
// Use of this source code is governed by the MIT license (see LICENSE file)

namespace BrewHub.Protocol.Mqtt;
public record MessagePayload
{
    private static int NextSeq = 1;
    public long Timestamp { get; init; }
    public int Seq { get; init; } = NextSeq++;
    public string? Model { get; init; }
    public Dictionary<string, object>? Metrics { get; init; }
}

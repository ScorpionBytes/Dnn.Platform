﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information
namespace DotNetNuke.Tests.BulkInstall.DeployClient;
using System;
using System.Collections.Generic;
using DotNetNuke.BulkInstall.DeployClient;
using Shouldly;

public class TestStopwatch : IStopwatch
{
    private readonly IReadOnlyList<TimeSpan> timeSpans;
    
    public TestStopwatch(params TimeSpan[] timeSpans)
    {
        this.timeSpans = timeSpans;
    }

    private int elapsedCalled;

    public TimeSpan Elapsed
    {
        get
        {
            this.IsStartNewCalled.ShouldBeTrue();

            if (this.timeSpans.Count == 0)
            {
                return TimeSpan.Zero;
            }

            return elapsedCalled >= this.timeSpans.Count 
                ? this.timeSpans[^1] 
                : timeSpans[elapsedCalled++];
        }
    }

    public bool IsStartNewCalled { get; private set; }

    public void StartNew()
    {
        this.IsStartNewCalled = true;
    }
}

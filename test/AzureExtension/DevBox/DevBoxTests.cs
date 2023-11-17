﻿// Copyright (c) Microsoft Corporation and Contributors
// Licensed under the MIT license.

using AzureExtension.Contracts;
using AzureExtension.DevBox;
using AzureExtension.Services.DevBox;
using AzureExtension.Test.DevBox;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Http;

namespace DevHomeAzureExtension.Test;

public partial class DevBoxTests
{
    [TestMethod]
    [TestCategory("Manual")]

    // [Ignore("Comment out to run")]
    public void DevBox_Parsing()
    {
        var host = Microsoft.Extensions.Hosting.Host.
            CreateDefaultBuilder().
            UseContentRoot(AppContext.BaseDirectory).
            UseDefaultServiceProvider((context, options) =>
            {
                options.ValidateOnBuild = true;
            }).
            ConfigureServices((context, services) =>
            {
                services.AddSingleton<IDevBoxManagementService, MgmtTestService>();
            }).
            Build();

        var instance = new DevBoxProvider(host);
        var systems = instance.GetComputeSystemsAsync().Result;
        Assert.IsTrue(systems.Any());
    }

    [TestMethod]
    [TestCategory("Manual")]

    // [Ignore("Comment out to run")]
    public void DevBoxAuth()
    {
        var host = Microsoft.Extensions.Hosting.Host.
            CreateDefaultBuilder().
            UseContentRoot(AppContext.BaseDirectory).
            UseDefaultServiceProvider((context, options) =>
            {
                options.ValidateOnBuild = true;
            }).
            ConfigureServices((context, services) =>
            {
                services.AddHttpClient();
                services.AddSingleton<IDevBoxManagementService, ManagementService>();
                services.AddSingleton<IDevBoxAuthService, AuthTestService>();
            }).
            Build();

        var instance = new DevBoxProvider(host);
        var systems = instance.GetComputeSystemsAsync().Result;
        Assert.IsTrue(systems.Any());
    }
}

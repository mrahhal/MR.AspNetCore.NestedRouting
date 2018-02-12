# MR.AspNetCore.NestedRouting

AppVeyor | Travis
---------|-------
[![Build status](https://img.shields.io/appveyor/ci/mrahhal/mr-aspnetcore-nestedrouting/master.svg)](https://ci.appveyor.com/project/mrahhal/mr-aspnetcore-nestedrouting) | [![Travis](https://img.shields.io/travis/mrahhal/MR.AspNetCore.NestedRouting/master.svg)](https://travis-ci.org/mrahhal/MR.AspNetCore.NestedRouting)

[![NuGet version](https://img.shields.io/nuget/v/MR.AspNetCore.NestedRouting.svg)](https://www.nuget.org/packages/MR.AspNetCore.NestedRouting)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://opensource.org/licenses/MIT)

Nested routing for Asp.Net Core.

## Overview

Enables writing nested controllers as a way to better model how api routes look.

## Getting started

### Configuration
```cs
public void ConfigureServices(IServiceCollection services)
{
    services
        .AddMvc()
        .AddNestedRouting();
}
```

You can also enable using kebab casing for routes:

```cs
public void ConfigureServices(IServiceCollection services)
{
    services
        .AddMvc()
        .AddNestedRouting(useKebabCase: true);
}
```

There is also a `ControllerNameAttribute` you can use to override controller names:

```
[ControllerName("bazzzzzz")]
public class BazController : SomeBaseController
{
}
```

## MvcPack

Using this together with [MvcPack](http://github.com/mrahhal/MR.AspNetCore.MvcPack) will give you a great way for organizing your controllers.

## Samples

- [`Basic`](/samples/Basic): implements nested routing in an Asp.Net Core app (has a swagger ui that you can open so as to inspect the routes).

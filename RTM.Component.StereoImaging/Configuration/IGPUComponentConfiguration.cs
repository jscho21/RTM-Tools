// RTM.Tools
// RTM.Component.StereoImaging
// IGPUComponentConfiguration.cs
// 
// Created by Bartosz Rachwal. 
// Copyright (c) 2015 Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved. 

using RTM.Component.StereoImaging.Configuration.Parameters;

namespace RTM.Component.StereoImaging.Configuration
{
    public interface IGPUComponentConfiguration : IConstantSpaceBPParameters, IDisparityBilateralFilterParameters,
        IComponentConfiguration
    {
    }
}
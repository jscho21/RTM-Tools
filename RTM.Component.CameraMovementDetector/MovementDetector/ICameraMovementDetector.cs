﻿// RTM.Tools
// RTM.Component.CameraMovementDetector
// ICameraMovementDetector.cs
// 
// Created by Bartosz Rachwal. 
// Copyright (c) 2015 Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved. 

using System;
using OpenRTM.Core;
using RTM.DTO;

namespace RTM.Component.CameraMovementDetector.MovementDetector
{
    public interface ICameraMovementDetector
    {
        CameraImage Image { get; set; }
        event EventHandler NewImage;

        Vectors Vectors { get; set; }
        event EventHandler NewVectors;

        void ProcessImage(CameraImage image);
    }
}
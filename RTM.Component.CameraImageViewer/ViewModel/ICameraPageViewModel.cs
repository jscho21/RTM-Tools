﻿// RTM.Tools
// RTM.Component.CameraImageViewer
// ICameraPageViewModel.cs
// 
// Created by Bartosz Rachwal. 
// Copyright (c) 2015 The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved. 

using System.Windows.Media;

namespace RTM.Component.CameraImageViewer.ViewModel
{
    public interface ICameraPageViewModel
    {
        ImageSource CameraImage { get; set; }
    }
}
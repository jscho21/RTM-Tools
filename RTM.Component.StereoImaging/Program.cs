﻿// RTM.Tools
// RTM.Component.StereoImaging
// Program.cs
// 
// Created by Bartosz Rachwal. 
// Copyright (c) 2015 Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved. 

using System;
using Microsoft.Practices.Unity;
using RTM.Calculator.OpticalFlow;
using RTM.Component.StereoImaging.Manager;
using RTM.Component.StereoImaging.Stereo;
using RTM.Converter.CameraImage;
using RTM.Detector.Features;

namespace RTM.Component.StereoImaging
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            RunComponent(args);
            WaitForExit();
        }

        private static void RunComponent(string[] args)
        {
            var container = new UnityContainer();

            container.RegisterType<IStereoImaging, Stereo.StereoImaging>(new ContainerControlledLifetimeManager());
            container.RegisterType<IOpticalFlow, OpticalFlowPyrLk>(new ContainerControlledLifetimeManager());
            container.RegisterType<IFeaturesDetector, FeaturesDetector>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICameraImageConverter, CameraImageConverter>(new ContainerControlledLifetimeManager());
            container.RegisterType<IComponentManager, ComponentManager>(new ContainerControlledLifetimeManager());

            container.Resolve<IComponentManager>().Start(args);
        }

        private static void WaitForExit()
        {
            var run = true;
            while (run)
            {
                var text = Console.ReadLine();
                if (string.IsNullOrEmpty(text))
                    continue;
                if (text.Trim().ToLower().Equals("exit"))
                {
                    run = false;
                }
            }
        }
    }
}
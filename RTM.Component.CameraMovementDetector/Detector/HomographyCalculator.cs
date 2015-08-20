// RTM.Tools
// RTM.Component.CameraMovementDetector
// HomographyCalculator.cs
// 
// Created by Bartosz Rachwal. 
// Copyright (c) 2015 Bartosz Rachwal. The National Institute of Advanced Industrial Science and Technology, Japan. All rights reserved. 
//
// SURF Feature Detector Reference 
// http://www.emgu.com/wiki/index.php/SURF_feature_detector_in_CSharp

using System;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.XFeatures2D;

namespace RTM.Component.CameraMovementDetector.Detector
{
    public class HomographyCalculator : IHomographyCalculator
    {
        private const int k = 2;
        private const int hessianThresh = 600;
        private const double uniquenessThreshold = 0.9;

        public Mat Calculate(Bitmap referenceBitmap, Bitmap currentBitmap)
        {
            Mat homography = null;
            using (var reference = new Image<Gray, byte>(referenceBitmap))
            using (var current = new Image<Gray, byte>(currentBitmap))
            using (var matches = new VectorOfVectorOfDMatch())
            {
                var modelKeyPoints = new VectorOfKeyPoint();
                var observedKeyPoints = new VectorOfKeyPoint();

                using (var uModelImage = reference.Mat.ToUMat(AccessType.Read))
                using (var uObservedImage = current.Mat.ToUMat(AccessType.Read))
                {
                    var surfCpu = new SURF(hessianThresh);

                    var modelDescriptors = new UMat();
                    surfCpu.DetectAndCompute(uModelImage, null, modelKeyPoints, modelDescriptors, false);

                    var observedDescriptors = new UMat();
                    surfCpu.DetectAndCompute(uObservedImage, null, observedKeyPoints, observedDescriptors, false);

                    var matcher = new BFMatcher(DistanceType.L2);
                    matcher.Add(modelDescriptors);
                    matcher.KnnMatch(observedDescriptors, matches, k, null);

                    var mask = new Mat(matches.Size, 1, DepthType.Cv8U, 1);
                    mask.SetTo(new MCvScalar(255));

                    Features2DToolbox.VoteForUniqueness(matches, uniquenessThreshold, mask);

                    var nonZeroCount = CvInvoke.CountNonZero(mask);
                    if (nonZeroCount < 4)
                    {
                        return null;
                    }
                    try
                    {
                        nonZeroCount = Features2DToolbox.VoteForSizeAndOrientation(modelKeyPoints, observedKeyPoints,
                            matches, mask, 1.5, 20);
                    }
                    catch (Exception)
                    {
                        return null;
                    }

                    if (nonZeroCount > 3)
                    {
                        homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(modelKeyPoints,
                            observedKeyPoints, matches, mask, 2);
                    }
                }
            }
            return homography;
        }
    }
}
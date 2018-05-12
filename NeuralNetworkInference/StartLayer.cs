﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagedCuda;

namespace NeuralNetworkInference
{
    public class StartLayer: Layer
    {
        CudaDeviceVariable<float> _data;

        public StartLayer(int width, int height, int channels, int batch)
            : base(0, 0, 0, width, height, channels, batch)
        {
            _data = new CudaDeviceVariable<float>(width * height * channels * batch);             
        }

        public void SetData(float[] data)
        {
            _data.CopyToDevice(data);
        }

        public void SetData(CudaDeviceVariable<float> data)
        {
            _data.CopyToDevice(data);
        }

        public override float Inference(CudaDeviceVariable<float> input)
        {
            SetData(input);
            return _nextLayer.Inference(_data);
        }
        
    }
}

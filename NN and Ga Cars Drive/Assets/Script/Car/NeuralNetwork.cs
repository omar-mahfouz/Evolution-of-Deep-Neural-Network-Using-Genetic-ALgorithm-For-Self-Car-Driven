using System;

public class NeuralNetwork
{
    public int[] layers;
    public double[] weights;
    public double[] biases;

    public NeuralNetwork(int[] layers, double[] weights, double[] biases)
    {
        if (layers.Length < 2)
        {
            return;
        }
        this.layers = layers;
        this.weights = weights;
        this.biases = biases;
    }

    public double[] Run(double[] inputs)
    {
        if (inputs.Length != layers[0])
        {
            return null;
        }

        int weightsIndex = 0;
        double[] prevLayer = inputs;

        for (int i = 1; i < layers.Length; i++)
        {
            double[] currentLayer = new double[layers[i]];

            for (int j = 0; j < layers[i]; j++)
            {
                double sum = 0;

                for (int h = 0; h < layers[i - 1]; h++)
                {
                    sum += prevLayer[h] * weights[weightsIndex];
                    weightsIndex++;
                }

                currentLayer[j] = Math.Tanh(sum + biases[i - 1]);
            }

            prevLayer = currentLayer;
        }

        return prevLayer;
    }

    public double Sigmoid(double x)
    {
        return 1 / (1 + Math.Exp(-x));
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuralNet_genotype
{
    // each neuron has a lot of dendryds - they provide initial values and setting up weight accordingly
    class dendryd
    {
        
        // each neuron can point to specific neuron - or can be the initial one taking data from initial array directly
        private neuron oNeuronRef;
        private float dendrydWeight;

        public void dendrydSetUp(float fWeight, neuron oNeuron)
        {
            dendrydWeight = fWeight;
            oNeuronRef = oNeuron;
        }
    }
}

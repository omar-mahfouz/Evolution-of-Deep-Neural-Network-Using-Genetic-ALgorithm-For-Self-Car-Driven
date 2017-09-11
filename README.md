# Evolution-of-Deep-Neural-Network-Using-Genetic-ALgorithm-For-Self-Car-Driven

A 3d Unity simulation for self car driven by Evolution Deep Neural Network Using Genetic Algorithm to train the neural network weights

You Can See The Implementation Video on YouTube : https://www.youtube.com/watch?v=QFjvmOMMdbs&feature=youtu.be


# The Car :
Cars Has a Five Front Facing Sensors Which Calculate The Distance to Walls and This Values is The Input of The Neural Network 
The Output of The Neural Network is Two Value The First For Engine Power And The Second for Turn Car Value


# The Neural Network :
I Have a Deep Neural Network,fully Connected With Feed Forward 
It Comprises 4 layers :
   - Input Layer with 5 nodes
   - Two Hidden Layers with 4 nodes For Each one
   - Output Layer with 2 nodes


# Training Neural Network :
I Train The Weights of the Neural Network using Evolutionary Algorithm (Genetic Algorithm)
At the first I Instantiate N Randomly Cars,When All Car Crashed I Sort The Cars Fitness Value And The Pick Up The Best Cars,
to Stay For The Next Generation and then i Make Cross Over For The Best Cars And Mutated it and then The Next Generation Start 
The Operations Repeat Until The Cars Reach The Goal

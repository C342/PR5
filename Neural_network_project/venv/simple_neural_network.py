import tensorflow as tf
from tensorflow.keras import layers, models

# Load and preprocess data
(train_images, train_labels), (test_images, test_labels) = tf.keras.datasets.mnist.load_data()
train_images, test_images = train_images / 255.0, test_images / 255.0

# Build the neural network model
model = models.Sequential([
    layers.Flatten(input_shape=(28, 28)),          # Input layer
    layers.Dense(128, activation='relu'),          # Hidden layer
    layers.Dense(10, activation='softmax')          # Output layer
])
# Compile the model
model.compile(optimizer='adam',
              loss='sparse_categorical_crossentropy',
             metrics=['accuracy'])
# Train the model
model.fit(train_images, train_labels, epochs=10)
# Evaluate the model on the test dataset
test_loss, test_acc = model.evaluate(test_images, test_labels, verbose=2)
print(f'Test accuracy: {test_acc}')
# Make predictions on new data
predictions = model.predict(test_images[:5])
# Print predicted labels for the first five test images
for i in range(5):
    print(f'Actual: {test_labels[i]}, Predicted: {tf.argmax(predictions[i])}')



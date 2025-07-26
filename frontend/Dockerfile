# Use Node.js base image
FROM node:20

# Set the working directory inside the container
WORKDIR /app

# Copy package.json and package-lock.json (if available)
COPY package*.json ./

# Install all dependencies
RUN npm install

# Copy the rest of the application source code
COPY . .

# Expose the port used by Angular
EXPOSE 4200

# Start the Angular application on port 5555
CMD ["npm", "run", "start-prod"]

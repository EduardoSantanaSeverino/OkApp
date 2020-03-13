#!/bin/bash
sudo echo "Welcome to building deployment for ok app application..."
sudo echo ""
sudo echo ""

cd ..
sudo echo "Building docker image eduardoamparo/okapp-host..."
sudo docker build -t eduardoamparo/okapp-host .
sudo echo "Done. -- Building docker image eduardoamparo/okapp-host..."
sudo echo ""
sudo echo ""

sudo echo "Pushing docker image eduardoamparo/okapp-host..."
sudo docker push eduardoamparo/okapp-host
sudo echo "Done. -- Pushing docker image eduardoamparo/okapp-host..."
sudo echo ""
sudo echo ""

cd ..
cd angular/
sudo echo "Remove node_modules"
sudo rm -rf node_modules
sudo echo "Building docker image eduardoamparo/okapp-ng..."
sudo docker build -t eduardoamparo/okapp-ng .
sudo echo "Done. -- Building docker image eduardoamparo/okapp-ng..."
sudo echo ""
sudo echo ""

sudo echo "Pushing docker image eduardoamparo/okapp-ng..."
sudo docker push eduardoamparo/okapp-ng
sudo echo "Done. -- Pushing docker image eduardoamparo/okapp-ng..."
sudo echo ""
sudo echo ""

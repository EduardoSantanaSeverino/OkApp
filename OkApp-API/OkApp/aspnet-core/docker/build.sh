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

# sudo echo "Pushing docker image eduardoamparo/okapp-host..."
# sudo docker push eduardoamparo/okapp-host
# sudo echo "Done. -- Pushing docker image eduardoamparo/okapp-host..."
# sudo echo ""
# sudo echo ""

# sudo echo "Loging to Remote server and restart application."
# ssh okapp "cd okapp && ./down.sh && ./up.sh"
# sudo echo ""


#dotnet build --runtime ubuntu.18.04-x64 --configuration Release --no-dependencies

sudo cp -r /home/ubuntu/sim/backend/. /opt/web-apps/demo/backend
sudo cp -r /home/ubuntu/sim/backend/. /opt/web-apps/cr/backend
sudo cp -r /home/ubuntu/sim/backend/. /opt/web-apps/chile/backend
sudo cp -r /home/ubuntu/sim/backend/. /opt/web-apps/panama/backend
sudo cp -r /home/ubuntu/sim/backend/. /opt/web-apps/mex/backend
pm2 restart all


#pm2 start "dotnet shipping-instruction-management.dll --environment demo" --name "demo-api"
#pm2 start "dotnet shipping-instruction-management.dll --environment cr" --name "cr-api"
#pm2 start "dotnet shipping-instruction-management.dll --environment chile" --name "chile-api"
#pm2 start "dotnet shipping-instruction-management.dll --environment panama" --name "panama-api"
#pm2 start "dotnet shipping-instruction-management.dll --environment mex" --name "mex-api"


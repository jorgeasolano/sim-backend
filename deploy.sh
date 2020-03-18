
#dotnet build --runtime ubuntu.18.04-x64 --configuration Release --no-dependencies
#dotnet publish -c Release --runtime ubuntu.18.04-x64 --self-contained false

sudo cp -r /home/ubuntu/sim/backend/. /opt/web-apps/demo/backend
sudo cp -r /home/ubuntu/sim/backend/. /opt/web-apps/cr/backend
sudo cp -r /home/ubuntu/sim/backend/. /opt/web-apps/chile/backend
sudo cp -r /home/ubuntu/sim/backend/. /opt/web-apps/panama/backend
sudo cp -r /home/ubuntu/sim/backend/. /opt/web-apps/mex/backend
sudo cp -r /home/ubuntu/sim/backend/. /opt/web-apps/ob-logistics/backend

pm2 restart all


#cd /opt/web-apps/demo/backend/ &&  pm2 start "dotnet shipping-instruction-management.dll --environment demo" --name "demo-api" && cd /
#pm2 start "dotnet shipping-instruction-management.dll --environment cr" --name "cr-api"
#pm2 start "dotnet shipping-instruction-management.dll --environment chile" --name "chile-api"
#pm2 start "dotnet shipping-instruction-management.dll --environment panama" --name "panama-api"
#pm2 start "dotnet shipping-instruction-management.dll --environment mex" --name "mex-api"
#pm2 start "dotnet shipping-instruction-management.dll --environment mex" --name "mex-api"
cd /opt/web-apps/ob-logistics/backend/ &&  pm2 start "dotnet shipping-instruction-management.dll --environment ob-logistics" --name "ob-logistics-api" && cd /

cd /opt/web-apps/demo/backend && dotnet --version

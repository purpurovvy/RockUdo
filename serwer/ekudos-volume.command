# create a storage
az storage account create --resource-group EkudosResourceGroup --name $STORAGE_ACCOUNT_NAME --sku Standard_LRS --location eastus

AZURE_STORAGE_CONNECTION_STRING=$(az storage account show-connection-string --resource-group EkudosResourceGroup --name $STORAGE_ACCOUNT_NAME --output tsv)
STORAGE_KEY=$(az storage account keys list --resource-group EkudosResourceGroup --account-name $STORAGE_ACCOUNT_NAME --query "[0].value" --output tsv)

# create a file share
az storage share create --name aci-share-demo

# add volume to instance/container
az container create --resource-group EkudosResourceGroup --name aci-demo-files --image microsoft/aci-hellofiles --location eastus --ports 80 --ip-address Public --azure-file-volume-account-name $STORAGE_ACCOUNT_NAME --azure-file-volume-account-key $STORAGE_KEY --azure-file-volume-share-name aci-share-demo --azure-file-volume-mount-path /aci/logs/

# show list of files
az storage file list -s aci-share-demo -o table

# download a file
az storage file download -s aci-share-demo -p <filename>
az container attach --resource-group EkudosResourceGroup --name mycontainer

# execute command
az container exec --resource-group EkudosResourceGroup --name mycontainer --exec-command /bin/sh

# show metrics
az monitor metrics list --resource EkudosResourceGroup --metric CPUUsage --output table
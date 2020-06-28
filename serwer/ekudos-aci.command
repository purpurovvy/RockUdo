# create test instances
az container create --resource-group EkudosResourceGroup --name rockudov001 --image ekudos.azurecr.io/rockudo-api:v0.0.1 --ip-address Public --location eastus --registry-login-server ekudos.azurecr.io --registry-username [user] --registry-password [pass] --dns-name-label rockudo-develop

# show list of containers
az container show --resource-group EkudosResourceGroup --name rockudov001 --query ipAddress.ip --output table

# show info about container
az container show --resource-group EkudosResourceGroup --name rockudov001 --query "{FQDN:ipAddress.fqdn,ProvisioningState:provisioningState}" --out table

# check status of container 
az container show --resource-group EkudosResourceGroup --name rockudov001 --query containers[0].instanceView.currentState.state

# show logs
az container logs --resource-group EkudosResourceGroup --name rockudov001

# during creating instance/container you can set
# normal environment variables
--environment-variables
# secure environment variables
--secure-environment-variables

# show variables
az container show --resource-group EkudosResourceGroup --name rockudov001 --query containers[0].environmentVariables
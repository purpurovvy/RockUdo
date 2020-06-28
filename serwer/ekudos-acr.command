# create acr for ekudos
az acr create --resource-group EkudosResourceGroup --name ekudos --sku Basic

# create img by acr
az acr build --registry ekudos --image rockudo-api:v0.0.1 .

# show repositories
az acr repository list --name ekudos --output table

# add access by admin and show login and pass
az acr update -n ekudos --admin-enabled true
az acr credential show --name ekudos

More about ACR
https://opdhsblobprod01.blob.core.windows.net/contents/4a6d75bb3af747de838e6ccc97c5d978/3c012d0bd96fc9dbd879ab3c9896ac6e?sv=2015-04-05&sr=b&sig=IW7ilUSu8QuH7TaTWSQQipOLYM%2FOKxLktO1kAi%2BENdo%3D&st=2019-03-05T20%3A19%3A47Z&se=2019-03-06T20%3A29%3A47Z&sp=r
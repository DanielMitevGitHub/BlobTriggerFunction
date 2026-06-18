az --version
 
az login

az account show
 
az group create --name BlobTriggerRG --location germanywestcentral

az group list

az group show --name BlobTriggerRG
 
az storage account create --name dmblobtrigger2026 --resource-group BlobTriggerRG --location germanywestcentral --sku Standard_LRS

az storage account list --resource-group BlobTriggerRG -o table

az storage account show --name dmblobtrigger2026 --resource-group BlobTriggerRG
 
$env:AZURE_STORAGE_CONNECTION_STRING = az storage account show-connection-string --name dmblobtrigger2026 --resource-group BlobTriggerRG --query connectionString --output tsv

$env:AZURE_STORAGE_CONNECTION_STRING
 
az storage container create --name farbbilder

az storage container create --name graubilder

az storage container list -o table

Test-Path "C:\bilder\bild1.jpg"
 
az storage blob upload -c farbbilder --name bild1.jpg --file "C:\bilder\bild1.jpg" --overwrite

az storage blob list -c graubilder -o table

az storage blob download -c graubilder --name bild1.jpg --file "C:\bilder\bild1.jpg" --overwrite
# contosoexpense

kubectl create ns conexp-devops

kubectl apply -f  admin-role.yaml  -n conexp-devops
kubectl apply -f  webhook-role.yaml  -n conexp-devops

kubectl apply -f create-ingress.yaml  -n conexp-devops
kubectl apply -f create-webhook.yaml  -n conexp-devops

kubectl apply -f github-secret.yaml -n conexp-devops

kubectl apply -f pipeline.yaml -n conexp-devops
kubectl apply -f triggers.yaml -n conexp-devops

kubectl apply -f webhook-run.yaml -n conexp-devops
kubectl apply -f ingress-run.yaml -n conexp-devops

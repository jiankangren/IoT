## Install Docker

```
curl -sSL https://get.docker.com | sudo -E sh
```

## Give Pi User Permission to run Docker

```
sudo usermod -aG docker pi
```

## Reboot

```
sudo reboot
```

## Test

```
docker run hello-world

```



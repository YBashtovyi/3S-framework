		------  1. Edit HOSTS  ------
hosts server:
62.205.138.250 rad.nchsh.ua
192.2.100.2 rad.nchsh.ua

hosts vpn client:
192.2.200.2 rad.nchsh.ua

hosts local network clients:
62.205.138.250 rad.nchsh.ua


c:/inetpub/wwwroot/mvp.view/app-566d6c7624 - edit --- отредактировать адрес сервера (ввести доменное им€)

Epxlorer run as Admin
go to site
install cert
or dowsnload cert and import -- mmc -> file --> add/remove snap in --> cert -- добать вручную сертификат

		------	2. Generate SSL with OpenSSL  ------
Install --> https://slproweb.com/products/Win32OpenSSL.html
control panel --> System --> Advanced system settings --> Environment Variables --> add Path : C:\OpenSSL-Win64\bin (or someth else)
Edit and Run Script : sslgen.bat
Run %name%.pfx and install it

SSL cert with makecert is not work with chrome 58+ and others browsers because can't create SAN
###generation ssl cert:
###cd C:\Program Files (x86)\Windows Kits\8.1\bin\x64
###Makecert Цr Цpe Цn CN="rad.nchsh.ua" Цb 05/10/2016 Цe 12/22/2019 Цeku 1.3.6.1.5.5.7.3.1 Цss my Цsr localmachine -sky exchange Цsp "Microsoft RSA SChannel Cryptographic Provider" Цsy 12

		------ 3. Import SSL cert to IIS (site) ------

iis  -->  rad.nchsh.ua --> edit bindings --> choose ssl cert
iis  -->  rad.nchsh.ua --> host: rad.nchsh.ua



Source code sslgen.bat :

REM PLEASE UPDATE THE FOLLOWING VARIABLES FOR YOUR NEEDS.
SET HOSTNAME=rad.nchsh
SET DOT=ua
SET COUNTRY=UA
SET STATE=KY
SET CITY=Kyiv
SET ORGANIZATION=IT
SET ORGANIZATION_UNIT=IT Department
SET EMAIL=webmaster@%HOSTNAME%.%DOT%

(
echo [req]
echo default_bits = 2048
echo prompt = no
echo default_md = sha256
echo x509_extensions = v3_req
echo distinguished_name = dn
echo:
echo [dn]
echo C = %COUNTRY%
echo ST = %STATE%
echo L = %CITY%
echo O = %ORGANIZATION%
echo OU = %ORGANIZATION_UNIT%
echo emailAddress = %EMAIL%
echo CN = %HOSTNAME%.%DOT%
echo:
echo [v3_req]
echo subjectAltName = @alt_names
echo:
echo [alt_names]
echo DNS.1 = *.%HOSTNAME%.%DOT%
echo DNS.2 = %HOSTNAME%.%DOT%
)>%HOSTNAME%.cnf

openssl req -new -x509 -newkey rsa:2048 -sha256 -nodes -keyout %HOSTNAME%.key -days 365 -out %HOSTNAME%.crt -config %HOSTNAME%.cnf
openssl pkcs12 -export -out %HOSTNAME%.pfx -inkey %HOSTNAME%.key -in %HOSTNAME%.crt -certfile %HOSTNAME%.crt
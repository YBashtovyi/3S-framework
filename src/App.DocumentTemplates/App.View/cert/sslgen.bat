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
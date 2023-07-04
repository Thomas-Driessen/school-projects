Ù
Ä/home/thomas/stack/School/Semester 3/Individueel project/minecraft-panel-api/minecraft-panel-api/Controllers/PlayerController.cs
	namespace 	
minecraft_panel_api
 
. 
Controllers )
{		 
[

 
ApiController

 
]

 
[ 
Route 

( 
$str $
)$ %
]% &
public 

class 
PlayerController !
:" #
ControllerBase$ 2
{ 
private "
MinecraftPluginContext &
_context' /
;/ 0
public 
PlayerController 
(  "
MinecraftPluginContext  6
context7 >
)> ?
{ 	
_context 
= 
context 
; 
} 	
[ 	
HttpGet	 
] 
public 
async 
Task 
< 
List 
< 
Player %
>% &
>& '

GetPlayers( 2
(2 3
)3 4
{ 	
return 
await 
_context !
.! "
Players" )
.) *
ToListAsync* 5
(5 6
)6 7
;7 8
} 	
[ 	
HttpPost	 
] 
public 
async 
Task 
< 
IActionResult '
>' (
CreatePlayer) 5
(5 6
Player6 <
player= C
)C D
{ 	
await 
_context 
. 
Players "
." #
AddAsync# +
(+ ,
player, 2
)2 3
;3 4
await 
_context 
. 
SaveChangesAsync +
(+ ,
), -
;- .
return   
Ok   
(   
)   
;   
}!! 	
}"" 
}## —
|/home/thomas/stack/School/Semester 3/Individueel project/minecraft-panel-api/minecraft-panel-api/Controllers/UnitTestTest.cs
	namespace 	
minecraft_panel_api
 
. 
Controllers )
{ 
public 

class 
UnitTestTest 
{ 
public 
async 
Task 
< 
string  
>  !
UnitTestTestMethod" 4
(4 5
)5 6
{ 	
return		 
$str		 
;		 
}

 	
} 
} î
~/home/thomas/stack/School/Semester 3/Individueel project/minecraft-panel-api/minecraft-panel-api/Controllers/UserController.cs
	namespace 	
minecraft_panel_api
 
. 
Controllers )
{ 
[		 
ApiController		 
]		 
[

 
Route

 

(

 
$str

 $
)

$ %
]

% &
public 

class 
UserController 
:  !
ControllerBase" 0
{ 
private 
readonly "
MinecraftPluginContext /
_context0 8
;8 9
public 
UserController 
( "
MinecraftPluginContext 4
context5 <
)< =
{ 	
_context 
= 
context 
; 
} 	
[ 	
HttpPost	 
( 
$str 
)  
]  !
public 
async 
Task 
< 
User 
> 
GetUserData  +
(+ ,
User, 0
user1 5
)5 6
{ 	
return 
await 
_context !
.! "
Users" '
.' (
FirstOrDefaultAsync( ;
(; <
x< =
=>> @
xA B
.B C
NameC G
==H J
userK O
.O P
NameP T
)T U
;U V
} 	
} 
} Ã

k/home/thomas/stack/School/Semester 3/Individueel project/minecraft-panel-api/minecraft-panel-api/Program.cs
	namespace

 	
minecraft_panel_api


 
{ 
public 

class 
Program 
{ 
public 
static 
void 
Main 
(  
string  &
[& '
]' (
args) -
)- .
{ 	
CreateHostBuilder 
( 
args "
)" #
.# $
Build$ )
() *
)* +
.+ ,
Run, /
(/ 0
)0 1
;1 2
} 	
public 
static 
IHostBuilder "
CreateHostBuilder# 4
(4 5
string5 ;
[; <
]< =
args> B
)B C
=>D F
Host 
.  
CreateDefaultBuilder %
(% &
args& *
)* +
. $
ConfigureWebHostDefaults )
() *

webBuilder* 4
=>5 7
{8 9

webBuilder: D
.D E

UseStartupE O
<O P
StartupP W
>W X
(X Y
)Y Z
;Z [
}\ ]
)] ^
;^ _
} 
} √ 
k/home/thomas/stack/School/Semester 3/Individueel project/minecraft-panel-api/minecraft-panel-api/Startup.cs
	namespace 	
minecraft_panel_api
 
{ 
public 

class 
Startup 
{ 
readonly 
string "
MyAllowSpecificOrigins .
=/ 0
$str1 J
;J K
public 
Startup 
( 
IConfiguration %
configuration& 3
)3 4
{ 	
Configuration 
= 
configuration )
;) *
} 	
public 
IConfiguration 
Configuration +
{, -
get. 1
;1 2
}3 4
public 
void 
ConfigureServices %
(% &
IServiceCollection& 8
services9 A
)A B
{   	
services!! 
.!! 
AddControllers!! #
(!!# $
)!!$ %
;!!% &
services## 
.## 
AddDbContext## !
<##! ""
MinecraftPluginContext##" 8
>##8 9
(##9 :
opt##: =
=>##> @
opt##A D
.##D E
UseMySql##E M
(##M N
Configuration##N [
.##[ \
GetConnectionString##\ o
(##o p
$str##p ~
)##~ 
)	## Ä
)
##Ä Å
;
##Å Ç
services%% 
.%% 
AddIdentity%%  
<%%  !
IdentityUser%%! -
,%%- .
IdentityRole%%/ ;
>%%; <
(%%< =
)%%= >
.&& 
AddRoles&& 
<&& 
IdentityRole&& &
>&&& '
(&&' (
)&&( )
.'' $
AddEntityFrameworkStores'' )
<'') *"
MinecraftPluginContext''* @
>''@ A
(''A B
)''B C
.(( $
AddDefaultTokenProviders(( )
((() *
)((* +
;((+ ,
services** 
.** 
	AddScoped** 
<** "
MinecraftPluginContext** 5
>**5 6
(**6 7
)**7 8
;**8 9
services,, 
.,, 
AddCors,, 
(,, 
options,, $
=>,,% '
{-- 
options.. 
... 
	AddPolicy.. !
(..! "
name.." &
:..& '"
MyAllowSpecificOrigins..( >
,..> ?
builder// 
=>// 
{00 
builder11 
.11  
WithOrigins11  +
(11+ ,
$str11, /
)11/ 0
;110 1
builder22 
.22  
WithHeaders22  +
(22+ ,
$str22, /
)22/ 0
;220 1
builder33 
.33  
WithMethods33  +
(33+ ,
$str33, /
)33/ 0
;330 1
}44 
)44 
;44 
}55 
)55 
;55 
}66 	
public99 
void99 
	Configure99 
(99 
IApplicationBuilder99 1
app992 5
,995 6
IWebHostEnvironment997 J
env99K N
)99N O
{:: 	
if;; 
(;; 
env;; 
.;; 
IsDevelopment;; !
(;;! "
);;" #
);;# $
{<< 
app== 
.== %
UseDeveloperExceptionPage== -
(==- .
)==. /
;==/ 0
}>> 
appBB 
.BB 

UseRoutingBB 
(BB 
)BB 
;BB 
appDD 
.DD 
UseAuthorizationDD  
(DD  !
)DD! "
;DD" #
appFF 
.FF 
UseCorsFF 
(FF "
MyAllowSpecificOriginsFF .
)FF. /
;FF/ 0
appHH 
.HH 
UseEndpointsHH 
(HH 
	endpointsHH &
=>HH' )
{HH* +
	endpointsHH, 5
.HH5 6
MapControllersHH6 D
(HHD E
)HHE F
;HHF G
}HHH I
)HHI J
;HHJ K
}II 	
}JJ 
}KK 
--用户表格
create table user_usersimp
(
	userid int identity(1,1) primary key,
	username nvarchar(12) not null,
	account varchar(32) not null,
	[password] varchar(32) not null,
	memberid int not null,
	head varchar(256) not null,
	delflag int not null,  --0正常，1冻结，2已经删除
)
create table user_userinfo
(
	userid int identity(1,1) primary key,
	email varchar(32) not null,
	isvalide int not null,--0未验证，1验证了
	number varchar(11) not null,
	[like] int not null,
	follower int not null,--追随者的数量
	follow int not null,--关注的人
	delflag int not null,--用户当前的状态
)
create table user_likeperson
(
	id int identity(1,1) primary key,
	userid int not null,
	userlikeid int not null,
	looked int not null,--0表示未查看，1表示已经查看了
	dateline datetime not null,--发生的时间点
)
create table user_likepost
(
	id int identity(1,1) primary key,
	postid int not null,
	userlikeid int not null,
	looked int not null,--用户是否被查看了
	dateline datetime not null
)
create table user_friendgroup
(
	gid int identity(1,1) primary key,
	userid int not null,
	title nvarchar(12) not null,
	dateline datetime not null, 
	--没有删除标记，删除就删除了
)
--添加好友，不需要写入额外的信息
create table user_friend
(
	id int identity(1,1) primary key,
	userid int not null,
	frinedid int not null,
	fusername nvarchar(12) not null,
	gid int not null,
	note varchar(12) not null,   --备注信息
	--加入时间和删除标记
	dateline datetime not null,
	DelFlag int not null,
)
create table user_friendrequest
(
	id int identity(1,1) primary key,
	friendid int not null,
	userid int not null,
	username varchar(12) not null,
	gid int not null,
	note nvarchar(32) not null,
	looked int not null, --是否已经被查看
	result int not null,--时候已经被处理了
	dateline datetime not null
)
create table user_friendoperation
(
	id int identity(1,1) primary key,
	userid int not null,
	friendid int not null,
	operationid int not null,
	dateline datetime not null,
)
create table user_operation
(
	id int identity(1,1) primary key,
	userid int not null,
	username nvarchar(12) not null,
	[action] varchar(12) not null,   --表示用户操作的名称
	eventid int not null,            --表示用户操作的具体表，比如表示用户的动作,likeperson likepost,post,comment,我们然后到这些表中找到数据
	delflag int not null,
	dateline datetime not null,
)

--给用户发送的消息,不能够被删除
create table user_message
(
	id int identity(1,1) primary key,
	content nvarchar(128) not null,
	userid int not null,
	writerid  int not null, --发送的用户端的id
	looked int not null, --用户时候时候已经被查看的
	dateline datetime not null,--发送的时间戳
)

--论坛表格
create table forum_board
(
	boardid int identity(1,1) primary key,
	title nvarchar(12) not null,
	[description] nvarchar(64) not null,
	delflag int not null,
	dateline datetime not null
)
create table forum_post
(
	postid int identity(1,1) primary key,
	title nvarchar(32) not null,
	content nvarchar(max) not null,
	userid int not null,
	[like] int not null,
	unlike int not null,
	boardid int not null,
	limitedmemberid int not null,
	dateline datetime not null,
	delflag int not null,
)
create table forum_comment
(
	commentid int identity(1,1) primary key,
	content nvarchar(62) not null,
	postid int not null,
	userid int not null,
	parentid int not null,
	delflag int not null,--删除标记
	dateline datetime not null,
)
create table forum_member
(
	memberid int identity(1,1) primary key,
	title nvarchar(12) not null,
)


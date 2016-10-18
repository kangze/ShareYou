--�û����
create table user_usersimp
(
	userid int identity(1,1) primary key,
	username nvarchar(12) not null,
	account varchar(32) not null,
	[password] varchar(32) not null,
	memberid int not null,
	head varchar(256) not null,
	delflag int not null,  --0������1���ᣬ2�Ѿ�ɾ��
)
create table user_userinfo
(
	userid int identity(1,1) primary key,
	email varchar(32) not null,
	isvalide int not null,--0δ��֤��1��֤��
	number varchar(11) not null,
	[like] int not null,
	follower int not null,--׷���ߵ�����
	follow int not null,--��ע����
	delflag int not null,--�û���ǰ��״̬
)
create table user_likeperson
(
	id int identity(1,1) primary key,
	userid int not null,
	userlikeid int not null,
	looked int not null,--0��ʾδ�鿴��1��ʾ�Ѿ��鿴��
	dateline datetime not null,--������ʱ���
)
create table user_likepost
(
	id int identity(1,1) primary key,
	postid int not null,
	userlikeid int not null,
	looked int not null,--�û��Ƿ񱻲鿴��
	dateline datetime not null
)
create table user_friendgroup
(
	gid int identity(1,1) primary key,
	userid int not null,
	title nvarchar(12) not null,
	dateline datetime not null, 
	--û��ɾ����ǣ�ɾ����ɾ����
)
--��Ӻ��ѣ�����Ҫд��������Ϣ
create table user_friend
(
	id int identity(1,1) primary key,
	userid int not null,
	frinedid int not null,
	fusername nvarchar(12) not null,
	gid int not null,
	note varchar(12) not null,   --��ע��Ϣ
	--����ʱ���ɾ�����
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
	looked int not null, --�Ƿ��Ѿ����鿴
	result int not null,--ʱ���Ѿ���������
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
	[action] varchar(12) not null,   --��ʾ�û�����������
	eventid int not null,            --��ʾ�û������ľ���������ʾ�û��Ķ���,likeperson likepost,post,comment,����Ȼ����Щ�����ҵ�����
	delflag int not null,
	dateline datetime not null,
)

--���û����͵���Ϣ,���ܹ���ɾ��
create table user_message
(
	id int identity(1,1) primary key,
	content nvarchar(128) not null,
	userid int not null,
	writerid  int not null, --���͵��û��˵�id
	looked int not null, --�û�ʱ��ʱ���Ѿ����鿴��
	dateline datetime not null,--���͵�ʱ���
)

--��̳���
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
	delflag int not null,--ɾ�����
	dateline datetime not null,
)
create table forum_member
(
	memberid int identity(1,1) primary key,
	title nvarchar(12) not null,
)


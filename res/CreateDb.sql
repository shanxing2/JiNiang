CREATE TABLE
IF NOT EXISTS DanmuFormat (
	Id INTEGER NOT NULL,
	Type TEXT,
	'Default' TEXT,
	Custom TEXT,
	Example TEXT,
	Memo TEXT,
	PRIMARY KEY (Id ASC)
);

CREATE TABLE
IF NOT EXISTS UserInfo (
	Id INTEGER NOT NULL,
	Nick TEXT NOT NULL,
	Cookies TEXT NOT NULL,
	SignDate TEXT,
	SignRewards TEXT,
	StoreCookies INTEGER,
	LastViewedRoomRealId INTEGER,
	RoomShortId INTEGER,
	RoomRealId INTEGER,
	PRIMARY KEY (Id ASC)
);

CREATE TABLE
IF NOT EXISTS ViewedRoomInfo (
	RealId INTEGER NOT NULL,
	ShortId INTEGER,
	UpName TEXT,
	LastAttentionCount INTEGER,
	LastViewedTimestamp INTEGER,
	DanmuTop100 TEXT,
	PRIMARY KEY (RealId ASC)
);

CREATE TABLE
IF NOT EXISTS Config (
	UserId INTEGER NOT NULL,
	ViewedRoomId INTEGER,
	Personal TEXT
);

CREATE UNIQUE INDEX
IF NOT EXISTS IK_UserId_ViewedRoomId ON Config (UserId ASC, ViewedRoomId ASC);

CREATE TABLE
IF NOT EXISTS RoomUsedTitle (
	Id INTEGER NOT NULL,
	RoomId INTEGER NOT NULL,
	Title TEXT,
	LastUseTimestamp INTEGER,
	PRIMARY KEY (Id ASC)
);

CREATE TABLE
IF NOT EXISTS Fans (
	UserId INTEGER NOT NULL,
	OriginalNick TEXT NOT NULL,
	RemarkNick TEXT,
	AttentTimestamp INTEGER,
	PRIMARY KEY (UserId ASC)
);

CREATE TABLE
IF NOT EXISTS DataStructureVersion (Version INTEGER NOT NULL);

CREATE TABLE
IF NOT EXISTS ViewerRemark (
	UserId INTEGER NOT NULL PRIMARY KEY,
	UserNick TEXT,
	Remark TEXT,
	CreatedTime TimeStamp NOT NULL DEFAULT (
		datetime('now', 'localtime')
	)
);

CREATE TABLE
IF NOT EXISTS Medal (
	UpUserId INTEGER NOT NULL PRIMARY KEY,
	UpUserNick TEXT,
	Detail TEXT
);


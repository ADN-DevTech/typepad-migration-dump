SET NOCOUNT ON
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IISDataReqMs](
	[ms] [bigint] NULL,
	[row] [bigint] NULL
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IISDataAllMs](
  [nbr] [bigint] NULL,
	[ms] [bigint] NULL
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[IISData](
	[dt] [datetime] NULL,
	[c_ip] [varchar](100) NULL,
	[c_ip_int] [bigint] NULL,
	[s_ip] [varchar](100) NULL,
	[adms_service] [varchar](100) NULL,
	[adms_method] [varchar](100) NULL,
	[adms_uid] [bigint] NULL,
	[adms_clientcmd] [varchar](100) NULL,
	[adms_vaultname] [varchar](50) NULL,
	[adms_session] [bigint] NULL,
	[adms_app] [varchar](25) NULL,
	[cs_method] [varchar](100) NULL,
	[sc_status] [int] NULL,
	[sc_bytes] [int] NULL,
	[cs_bytes] [int] NULL,
	[time_taken] [int] NULL,
	[command_type] [varchar](50) NULL,
	[start_ms] [bigint] NULL,
	[stop_ms] [bigint] NULL,
  [row] [bigint] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[TimeToTicks] (@hour int, @minute int, @second int)  
RETURNS bigint 
WITH SCHEMABINDING
AS 
-- converts the given hour/minute/second to the corresponding ticks
BEGIN 
RETURN (((@hour * 3600) + CONVERT(bigint, @minute) * 60) + CONVERT(bigint, @second)) * 10000000
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[MonthToDays366] (@month int)
RETURNS int 
WITH SCHEMABINDING
AS
-- converts the given month (0-12) to the corresponding number of days into the year (by end of month)
-- this function is for leap years
BEGIN 
RETURN
	CASE @month
		WHEN 0 THEN 0
		WHEN 1 THEN 31
		WHEN 2 THEN 60
		WHEN 3 THEN 91
		WHEN 4 THEN 121
		WHEN 5 THEN 152
		WHEN 6 THEN 182
		WHEN 7 THEN 213
		WHEN 8 THEN 244
		WHEN 9 THEN 274
		WHEN 10 THEN 305
		WHEN 11 THEN 335
		WHEN 12 THEN 366
		ELSE 0
	END
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[MonthToDays365] (@month int)
RETURNS int
WITH SCHEMABINDING
AS
-- converts the given month (0-12) to the corresponding number of days into the year (by end of month)
-- this function is for non-leap years
BEGIN 
RETURN
	CASE @month
		WHEN 0 THEN 0
		WHEN 1 THEN 31
		WHEN 2 THEN 59
		WHEN 3 THEN 90
		WHEN 4 THEN 120
		WHEN 5 THEN 151
		WHEN 6 THEN 181
		WHEN 7 THEN 212
		WHEN 8 THEN 243
		WHEN 9 THEN 273
		WHEN 10 THEN 304
		WHEN 11 THEN 334
		WHEN 12 THEN 365
		ELSE 0
	END
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[MonthToDays] (@year int, @month int)
RETURNS int
WITH SCHEMABINDING
AS
-- converts the given month (0-12) to the corresponding number of days into the year (by end of month)
-- this function is for non-leap years
BEGIN 
RETURN 
	-- determine whether the given year is a leap year
	CASE 
		WHEN (@year % 4 = 0) and ((@year % 100  != 0) or ((@year % 100 = 0) and (@year % 400 = 0))) THEN dbo.MonthToDays366(@month)
		ELSE dbo.MonthToDays365(@month)
	END
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[DateToTicks] (@year int, @month int, @day int)
RETURNS bigint
WITH SCHEMABINDING
AS
-- converts the given year/month/day to the corresponding ticks
BEGIN 
RETURN CONVERT(bigint, (((((((@year - 1) * 365) + ((@year - 1) / 4)) - ((@year - 1) / 100)) + ((@year - 1) / 400)) + dbo.MonthToDays(@year, @month - 1)) + @day) - 1) * 864000000000;
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[DateTimeToTicks] (@d datetime)
RETURNS bigint
WITH SCHEMABINDING
AS
-- converts the given datetime to .NET-compatible ticks
-- see http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpref/html/frlrfsystemdatetimeclasstickstopic.asp
BEGIN 
RETURN 
	dbo.DateToTicks(DATEPART(yyyy, @d), DATEPART(mm, @d), DATEPART(dd, @d)) +
	dbo.TimeToTicks(DATEPART(hh, @d), DATEPART(mi, @d), DATEPART(ss, @d)) +
	(CONVERT(bigint, DATEPART(ms, @d)) * CONVERT(bigint,10000));
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[TicksToDateTime]
(
        @TickValue BIGINT
)
RETURNS DATETIME
WITH SCHEMABINDING
AS
BEGIN
        -- Declare the return variable here
        DECLARE @Result DATETIME
        -- Add the T-SQL statements to compute the return value here
        DECLARE @Days FLOAT
        SELECT @Days = @TickValue * POWER(10.00000000000,-7) / 60 / 60 / 24
        SELECT @Result = DATEADD(d, CAST(@Days AS INT), CAST('0001-01-01' AS DATE)) + CAST( (@Days - FLOOR(@Days)) AS DATETIME)
        -- Return the result of the function
        RETURN @Result
END
GO
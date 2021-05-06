/*
 Navicat Premium Data Transfer

 Source Server         : SqlServer
 Source Server Type    : SQL Server
 Source Server Version : 12002000
 Source Host           : .\SQLExpress:1433
 Source Catalog        : UCOnlinePortal
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 12002000
 File Encoding         : 65001

 Date: 06/05/2021 20:25:55
*/


-- ----------------------------
-- Table structure for 212assessment_be
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[212assessment_be]') AND type IN ('U'))
	DROP TABLE [dbo].[212assessment_be]
GO

CREATE TABLE [dbo].[212assessment_be] (
  [assess_be_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [old_account] float(53) DEFAULT ((0)) NOT NULL,
  [fee_tuition] float(53) DEFAULT ((0)) NOT NULL,
  [fee_lab] float(53) DEFAULT ((0)) NOT NULL,
  [fee_reg] float(53) DEFAULT ((0)) NOT NULL,
  [fee_misc_others] float(53) DEFAULT ((0)) NOT NULL,
  [fee_total] float(53) DEFAULT ((0)) NOT NULL,
  [total_due] float(53) DEFAULT ((0)) NOT NULL,
  [payment] float(53) DEFAULT ((0)) NOT NULL,
  [discount] float(53) DEFAULT ((0)) NOT NULL,
  [adjustment] float(53) DEFAULT ((0)) NOT NULL,
  [balance] float(53) DEFAULT ((0)) NOT NULL,
  [stud_share] float(53) DEFAULT ((0)) NOT NULL,
  [stud_share_bal] float(53)  NOT NULL,
  [amount_due] float(53) DEFAULT ((0)) NOT NULL,
  [exam] varchar(1) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[212assessment_be] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for 212assessment_cl
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[212assessment_cl]') AND type IN ('U'))
	DROP TABLE [dbo].[212assessment_cl]
GO

CREATE TABLE [dbo].[212assessment_cl] (
  [assess_cl_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [old_account] float(53)  NOT NULL,
  [fee_tuition] float(53) DEFAULT ((0)) NOT NULL,
  [fee_lab] float(53) DEFAULT ((0)) NOT NULL,
  [fee_reg] float(53) DEFAULT ((0)) NOT NULL,
  [fee_misc] float(53) DEFAULT ((0)) NOT NULL,
  [assess_total] float(53) DEFAULT ((0)) NOT NULL,
  [excess_payment] float(53)  NULL,
  [payment] float(53) DEFAULT ((0)) NOT NULL,
  [discount] float(53) DEFAULT ((0)) NOT NULL,
  [adjustment] float(53) DEFAULT ((0)) NOT NULL,
  [adjustment_credit] float(53)  NULL,
  [adjustment_debit] float(53)  NULL,
  [balance] float(53) DEFAULT ((0)) NOT NULL,
  [amount_due] float(53) DEFAULT ((0)) NOT NULL,
  [exam] varchar(1) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[212assessment_cl] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for 212assessment_sh
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[212assessment_sh]') AND type IN ('U'))
	DROP TABLE [dbo].[212assessment_sh]
GO

CREATE TABLE [dbo].[212assessment_sh] (
  [assess_sh_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [old_account] float(53) DEFAULT ((0)) NOT NULL,
  [fee_tuition] float(53) DEFAULT ((0)) NOT NULL,
  [fee_lab] float(53) DEFAULT ((0)) NOT NULL,
  [fee_reg] float(53) DEFAULT ((0)) NOT NULL,
  [fee_misc_others] float(53) DEFAULT ((0)) NOT NULL,
  [fee_total] float(53) DEFAULT ((0)) NOT NULL,
  [total_due] float(53) DEFAULT ((0)) NOT NULL,
  [payment] float(53) DEFAULT ((0)) NOT NULL,
  [excess_payment] float(53)  NULL,
  [discount] float(53) DEFAULT ((0)) NOT NULL,
  [adjustment] float(53) DEFAULT ((0)) NOT NULL,
  [adjustment_credit] float(53)  NULL,
  [adjustment_debit] float(53)  NULL,
  [government_subsidy] float(53)  NULL,
  [balance] float(53) DEFAULT ((0)) NOT NULL,
  [stud_share] float(53) DEFAULT ((0)) NOT NULL,
  [stud_share_bal] float(53)  NOT NULL,
  [amount_due] float(53) DEFAULT ((0)) NOT NULL,
  [exam] varchar(1) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[212assessment_sh] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for 212attachments
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[212attachments]') AND type IN ('U'))
	DROP TABLE [dbo].[212attachments]
GO

CREATE TABLE [dbo].[212attachments] (
  [attachment_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(11) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [email] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [type] varchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [filename] varchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [acknowledged] smallint  NULL
)
GO

ALTER TABLE [dbo].[212attachments] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for 212contact_address
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[212contact_address]') AND type IN ('U'))
	DROP TABLE [dbo].[212contact_address]
GO

CREATE TABLE [dbo].[212contact_address] (
  [add_con_id] int  IDENTITY(1,1) NOT NULL,
  [stud_info_id] int  NOT NULL,
  [p_country] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [p_province] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [p_city] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [p_barangay] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [p_street] varchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [p_zip] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [c_province] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [c_city] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [c_barangay] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [c_street] varchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [mobile] varchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [landline] varchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [email] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [facebook] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[212contact_address] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for 212core_be
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[212core_be]') AND type IN ('U'))
	DROP TABLE [dbo].[212core_be]
GO

CREATE TABLE [dbo].[212core_be] (
  [core_be_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [innovation_1] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [innovation_2] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [innovation_3] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [camaraderie] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [alignment] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [respect] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [excellence] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [exam] varchar(1) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[212core_be] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for 212core_sh
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[212core_sh]') AND type IN ('U'))
	DROP TABLE [dbo].[212core_sh]
GO

CREATE TABLE [dbo].[212core_sh] (
  [core_sh_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [innovation_1] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [innovation_2] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [innovation_3] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [camaraderie] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [alignment] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [respect] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [excellence] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [exam] varchar(1) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[212core_sh] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for 212exam_promissory
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[212exam_promissory]') AND type IN ('U'))
	DROP TABLE [dbo].[212exam_promissory]
GO

CREATE TABLE [dbo].[212exam_promissory] (
  [exam_promi_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [request_prelim] smallint  NULL,
  [request_prelim_amount] int  NULL,
  [request_prelim_date] datetime  NULL,
  [prelim_promi_id] int  NULL,
  [request_midterm] smallint  NULL,
  [request_midterm_amount] int  NULL,
  [request_midterm_date] datetime  NULL,
  [midterm_promi_id] int  NULL,
  [request_semi] smallint  NULL,
  [request_semi_amount] int  NULL,
  [request_semi_date] datetime  NULL,
  [semi_promi_id] int  NULL,
  [request_final] smallint  NULL,
  [request_final_amount] int  NULL,
  [request_final_date] datetime  NULL,
  [final_promi_id] int  NULL
)
GO

ALTER TABLE [dbo].[212exam_promissory] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for 212family_info
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[212family_info]') AND type IN ('U'))
	DROP TABLE [dbo].[212family_info]
GO

CREATE TABLE [dbo].[212family_info] (
  [family_info_id] int  IDENTITY(1,1) NOT NULL,
  [stud_info_id] int  NOT NULL,
  [father_name] varchar(120) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [father_contact] varchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [father_occupation] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [mother_name] varchar(120) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [mother_contact] varchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [mother_occupation] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [guardian_name] varchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [guardian_contact] varchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [guardian_occupation] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[212family_info] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for 212grades_be
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[212grades_be]') AND type IN ('U'))
	DROP TABLE [dbo].[212grades_be]
GO

CREATE TABLE [dbo].[212grades_be] (
  [grades_be_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(11) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [edp_code] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [dte] datetime  NOT NULL,
  [grade_1] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [grade_2] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [grade_3] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [grade_4] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[212grades_be] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for 212grades_cl
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[212grades_cl]') AND type IN ('U'))
	DROP TABLE [dbo].[212grades_cl]
GO

CREATE TABLE [dbo].[212grades_cl] (
  [grades_cl_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(11) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [edp_code] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [dte] datetime  NOT NULL,
  [grade_1] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [grade_2] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[212grades_cl] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for 212grades_sh
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[212grades_sh]') AND type IN ('U'))
	DROP TABLE [dbo].[212grades_sh]
GO

CREATE TABLE [dbo].[212grades_sh] (
  [grades_sh_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(11) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [edp_code] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [dte] datetime  NOT NULL,
  [grade_1] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [grade_2] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[212grades_sh] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for 212oenrp
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[212oenrp]') AND type IN ('U'))
	DROP TABLE [dbo].[212oenrp]
GO

CREATE TABLE [dbo].[212oenrp] (
  [oenrp_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [year_level] smallint  NOT NULL,
  [course_code] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [enrollment_date] datetime  NULL,
  [units] smallint  NOT NULL,
  [classification] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [dept] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [mdn] varchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [section] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [status] smallint  NOT NULL,
  [request_overload] smallint  NULL,
  [request_deblock] smallint  NULL,
  [request_promissory] smallint DEFAULT ((0)) NOT NULL,
  [promi_pay] int DEFAULT ((0)) NOT NULL,
  [registered_on] datetime  NULL,
  [submitted_on] datetime  NULL,
  [approved_reg_registrar] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [approved_reg_registrar_on] datetime  NULL,
  [disapproved_reg_registrar] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [disapproved_reg_registrar_on] datetime  NULL,
  [approved_reg_dean] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [approved_reg_dean_on] datetime  NULL,
  [disapproved_reg_dean] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [disapproved_reg_dean_on] datetime  NULL,
  [approved_dean] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [approved_dean_on] datetime  NULL,
  [disapproved_dean] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [disapproved_dean_on] datetime  NULL,
  [adjustment_count] smallint  NOT NULL,
  [adjustment_by] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [adjustment_on] datetime  NULL,
  [approved_acctg] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [approved_acctg_on] datetime  NULL,
  [needed_payment] int  NULL,
  [approved_cashier] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [approved_cashier_on] datetime  NULL
)
GO

ALTER TABLE [dbo].[212oenrp] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for 212ostsp
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[212ostsp]') AND type IN ('U'))
	DROP TABLE [dbo].[212ostsp]
GO

CREATE TABLE [dbo].[212ostsp] (
  [sts_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [edp_code] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [status] smallint  NOT NULL,
  [remarks] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [adjusted_on] datetime  NOT NULL
)
GO

ALTER TABLE [dbo].[212ostsp] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for 212promissory
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[212promissory]') AND type IN ('U'))
	DROP TABLE [dbo].[212promissory]
GO

CREATE TABLE [dbo].[212promissory] (
  [promi_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [promi_message] varchar(1500) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [promi_date] datetime  NOT NULL
)
GO

ALTER TABLE [dbo].[212promissory] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for 212schedules
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[212schedules]') AND type IN ('U'))
	DROP TABLE [dbo].[212schedules]
GO

CREATE TABLE [dbo].[212schedules] (
  [schedule_id] int  IDENTITY(1,1) NOT NULL,
  [edp_code] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [description] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [internal_code] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [sub_type] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [units] smallint  NOT NULL,
  [time_start] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [time_end] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [mdn] varchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [days] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [m_time_start] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [m_time_end] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [m_days] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [size] int  NOT NULL,
  [pending_enrolled] int  NULL,
  [official_enrolled] int  NULL,
  [max_size] int  NOT NULL,
  [instructor] varchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [course_code] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [section] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [room] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [instructor_2] varchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [deployed] smallint  NOT NULL,
  [status] smallint  NOT NULL,
  [split_type] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [split_code] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [is_gened] smallint  NULL
)
GO

ALTER TABLE [dbo].[212schedules] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for 212school_info
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[212school_info]') AND type IN ('U'))
	DROP TABLE [dbo].[212school_info]
GO

CREATE TABLE [dbo].[212school_info] (
  [school_info_id] int  IDENTITY(1,1) NOT NULL,
  [stud_info_id] int  NOT NULL,
  [elem_name] varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [elem_year] varchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [elem_last_year] smallint  NULL,
  [elem_type] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [elem_lrn_no] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [elem_esc_student_id] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [elem_esc_school_id] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [sec_name] varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [sec_year] varchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [sec_last_year] smallint  NULL,
  [sec_last_strand] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [sec_type] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [sec_lrn_no] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [sec_esc_student_id] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [sec_esc_school_id] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [col_name] varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [col_year] varchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [col_course] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [col_last_year] smallint  NULL
)
GO

ALTER TABLE [dbo].[212school_info] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for 212student_info
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[212student_info]') AND type IN ('U'))
	DROP TABLE [dbo].[212student_info]
GO

CREATE TABLE [dbo].[212student_info] (
  [stud_info_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [course_code] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [year_level] int  NOT NULL,
  [mdn] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [first_name] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [last_name] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [middle_name] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [suffix] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [gender] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [status] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [nationality] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [birth_date] datetime  NOT NULL,
  [birth_place] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [religion] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [date_created] datetime  NOT NULL,
  [date_updated] datetime  NOT NULL,
  [start_term] smallint  NOT NULL,
  [is_verified] smallint  NOT NULL,
  [token] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [classification] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [dept] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[212student_info] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for adviser_section
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[adviser_section]') AND type IN ('U'))
	DROP TABLE [dbo].[adviser_section]
GO

CREATE TABLE [dbo].[adviser_section] (
  [section_ad_id] int  IDENTITY(1,1) NOT NULL,
  [section] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [instructor] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [department] varchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [active_term] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[adviser_section] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for assessment_be
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[assessment_be]') AND type IN ('U'))
	DROP TABLE [dbo].[assessment_be]
GO

CREATE TABLE [dbo].[assessment_be] (
  [assess_be_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [old_account] float(53) DEFAULT ((0)) NOT NULL,
  [fee_tuition] float(53) DEFAULT ((0)) NOT NULL,
  [fee_lab] float(53) DEFAULT ((0)) NOT NULL,
  [fee_reg] float(53) DEFAULT ((0)) NOT NULL,
  [fee_misc_others] float(53) DEFAULT ((0)) NOT NULL,
  [fee_total] float(53) DEFAULT ((0)) NOT NULL,
  [total_due] float(53) DEFAULT ((0)) NOT NULL,
  [payment] float(53) DEFAULT ((0)) NOT NULL,
  [discount] float(53) DEFAULT ((0)) NOT NULL,
  [adjustment] float(53) DEFAULT ((0)) NOT NULL,
  [balance] float(53) DEFAULT ((0)) NOT NULL,
  [stud_share] float(53) DEFAULT ((0)) NOT NULL,
  [stud_share_bal] float(53)  NOT NULL,
  [amount_due] float(53) DEFAULT ((0)) NOT NULL,
  [exam] varchar(1) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [active_term] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[assessment_be] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for assessment_cl
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[assessment_cl]') AND type IN ('U'))
	DROP TABLE [dbo].[assessment_cl]
GO

CREATE TABLE [dbo].[assessment_cl] (
  [assess_cl_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [old_account] float(53) DEFAULT ((0)) NOT NULL,
  [fee_tuition] float(53) DEFAULT ((0)) NOT NULL,
  [fee_lab] float(53) DEFAULT ((0)) NOT NULL,
  [fee_reg] float(53) DEFAULT ((0)) NOT NULL,
  [fee_misc] float(53) DEFAULT ((0)) NOT NULL,
  [assess_total] float(53) DEFAULT ((0)) NOT NULL,
  [excess_payment] float(53)  NULL,
  [payment] float(53) DEFAULT ((0)) NOT NULL,
  [discount] float(53) DEFAULT ((0)) NOT NULL,
  [adjustment] float(53) DEFAULT ((0)) NOT NULL,
  [adjustment_credit] float(53)  NULL,
  [adjustment_debit] float(53)  NULL,
  [balance] float(53) DEFAULT ((0)) NOT NULL,
  [amount_due] float(53) DEFAULT ((0)) NOT NULL,
  [exam] varchar(1) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [active_term] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[assessment_cl] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for assessment_sh
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[assessment_sh]') AND type IN ('U'))
	DROP TABLE [dbo].[assessment_sh]
GO

CREATE TABLE [dbo].[assessment_sh] (
  [assess_sh_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [old_account] float(53) DEFAULT ((0)) NOT NULL,
  [fee_tuition] float(53) DEFAULT ((0)) NOT NULL,
  [fee_lab] float(53) DEFAULT ((0)) NOT NULL,
  [fee_reg] float(53) DEFAULT ((0)) NOT NULL,
  [fee_misc_others] float(53) DEFAULT ((0)) NOT NULL,
  [fee_total] float(53) DEFAULT ((0)) NOT NULL,
  [total_due] float(53) DEFAULT ((0)) NOT NULL,
  [payment] float(53) DEFAULT ((0)) NOT NULL,
  [excess_payment] float(53)  NULL,
  [discount] float(53) DEFAULT ((0)) NOT NULL,
  [adjustment] float(53) DEFAULT ((0)) NOT NULL,
  [adjustment_credit] float(53)  NULL,
  [adjustment_debit] float(53)  NULL,
  [government_subsidy] float(53)  NULL,
  [balance] float(53) DEFAULT ((0)) NOT NULL,
  [stud_share] float(53) DEFAULT ((0)) NOT NULL,
  [stud_share_bal] float(53)  NOT NULL,
  [amount_due] float(53) DEFAULT ((0)) NOT NULL,
  [exam] varchar(1) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [active_term] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[assessment_sh] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for attachments
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[attachments]') AND type IN ('U'))
	DROP TABLE [dbo].[attachments]
GO

CREATE TABLE [dbo].[attachments] (
  [attachment_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(11) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [email] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [type] varchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [filename] varchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [acknowledged] smallint  NULL,
  [active_term] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[attachments] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for config
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[config]') AND type IN ('U'))
	DROP TABLE [dbo].[config]
GO

CREATE TABLE [dbo].[config] (
  [config_id] int  IDENTITY(1,1) NOT NULL,
  [sequence] int  NOT NULL,
  [id_year] smallint  NOT NULL,
  [campus_id] smallint  NOT NULL,
  [campus_lms] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [prelim] datetime  NULL,
  [midterm] datetime  NULL,
  [semifinal] datetime  NULL,
  [final] datetime  NULL,
  [basic_start] int  NOT NULL,
  [basic_end] int  NOT NULL,
  [permit_cutoff] int  NOT NULL,
  [grade1_due] datetime  NULL,
  [grade2_due] datetime  NULL,
  [active_term] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [active_terms] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[config] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for contact_address
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[contact_address]') AND type IN ('U'))
	DROP TABLE [dbo].[contact_address]
GO

CREATE TABLE [dbo].[contact_address] (
  [add_con_id] int  IDENTITY(1,1) NOT NULL,
  [stud_info_id] int  NOT NULL,
  [p_country] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [p_province] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [p_city] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [p_barangay] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [p_street] varchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [p_zip] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [c_province] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [c_city] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [c_barangay] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [c_street] varchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [mobile] varchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [landline] varchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [email] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [facebook] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [active_term] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[contact_address] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for core_be
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[core_be]') AND type IN ('U'))
	DROP TABLE [dbo].[core_be]
GO

CREATE TABLE [dbo].[core_be] (
  [core_be_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [innovation_1] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [innovation_2] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [innovation_3] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [camaraderie] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [alignment] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [respect] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [excellence] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [exam] varchar(1) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [active_term] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[core_be] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for core_sh
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[core_sh]') AND type IN ('U'))
	DROP TABLE [dbo].[core_sh]
GO

CREATE TABLE [dbo].[core_sh] (
  [core_sh_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [innovation_1] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [innovation_2] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [innovation_3] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [camaraderie] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [alignment] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [respect] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [excellence] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [exam] varchar(1) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [active_term] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[core_sh] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for course_list
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[course_list]') AND type IN ('U'))
	DROP TABLE [dbo].[course_list]
GO

CREATE TABLE [dbo].[course_list] (
  [course_id] smallint  IDENTITY(1,1) NOT NULL,
  [course_code] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [course_description] varchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [course_abbr] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [course_year_limit] smallint  NOT NULL,
  [course_department] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [course_department_abbr] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [department] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [course_active] smallint  NOT NULL,
  [enrollment_open] smallint  NULL,
  [adjustment_start] datetime  NULL,
  [adjustment_end] datetime  NULL,
  [active_term] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[course_list] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for curriculum
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[curriculum]') AND type IN ('U'))
	DROP TABLE [dbo].[curriculum]
GO

CREATE TABLE [dbo].[curriculum] (
  [curr_id] int  IDENTITY(1,1) NOT NULL,
  [year] smallint  NULL,
  [isDeployed] smallint  NULL
)
GO

ALTER TABLE [dbo].[curriculum] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for equivalence
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[equivalence]') AND type IN ('U'))
	DROP TABLE [dbo].[equivalence]
GO

CREATE TABLE [dbo].[equivalence] (
  [equival_id] int  IDENTITY(1,1) NOT NULL,
  [internal_code] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [equival_code] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[equivalence] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for exam_promissory
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[exam_promissory]') AND type IN ('U'))
	DROP TABLE [dbo].[exam_promissory]
GO

CREATE TABLE [dbo].[exam_promissory] (
  [exam_promi_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [request_prelim] smallint  NULL,
  [request_prelim_amount] int  NULL,
  [request_prelim_date] datetime  NULL,
  [prelim_promi_id] int  NULL,
  [request_midterm] smallint  NULL,
  [request_midterm_amount] int  NULL,
  [request_midterm_date] datetime  NULL,
  [midterm_promi_id] int  NULL,
  [request_semi] smallint  NULL,
  [request_semi_amount] int  NULL,
  [request_semi_date] datetime  NULL,
  [semi_promi_id] int  NULL,
  [request_final] smallint  NULL,
  [request_final_amount] int  NULL,
  [request_final_date] datetime  NULL,
  [final_promi_id] int  NULL,
  [active_term] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[exam_promissory] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for family_info
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[family_info]') AND type IN ('U'))
	DROP TABLE [dbo].[family_info]
GO

CREATE TABLE [dbo].[family_info] (
  [family_info_id] int  IDENTITY(1,1) NOT NULL,
  [stud_info_id] int  NOT NULL,
  [father_name] varchar(120) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [father_contact] varchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [father_occupation] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [mother_name] varchar(120) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [mother_contact] varchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [mother_occupation] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [guardian_name] varchar(150) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [guardian_contact] varchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [guardian_occupation] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [active_term] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[family_info] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for grade_evaluation
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[grade_evaluation]') AND type IN ('U'))
	DROP TABLE [dbo].[grade_evaluation]
GO

CREATE TABLE [dbo].[grade_evaluation] (
  [grade_eval_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [int_code] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [midterm_grade] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [final_grade] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [term] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[grade_evaluation] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for grade_evaluation_be
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[grade_evaluation_be]') AND type IN ('U'))
	DROP TABLE [dbo].[grade_evaluation_be]
GO

CREATE TABLE [dbo].[grade_evaluation_be] (
  [grade_eval_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [int_code] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [grade_1] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [grade_2] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [grade_3] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [grade_4] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [term] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[grade_evaluation_be] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for grades_be
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[grades_be]') AND type IN ('U'))
	DROP TABLE [dbo].[grades_be]
GO

CREATE TABLE [dbo].[grades_be] (
  [grades_be_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(11) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [edp_code] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [dte] datetime  NOT NULL,
  [grade_1] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [grade_2] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [grade_3] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [grade_4] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [active_term] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[grades_be] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for grades_cl
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[grades_cl]') AND type IN ('U'))
	DROP TABLE [dbo].[grades_cl]
GO

CREATE TABLE [dbo].[grades_cl] (
  [grades_cl_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(11) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [edp_code] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [dte] datetime  NOT NULL,
  [grade_1] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [grade_2] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [active_term] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[grades_cl] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for grades_sh
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[grades_sh]') AND type IN ('U'))
	DROP TABLE [dbo].[grades_sh]
GO

CREATE TABLE [dbo].[grades_sh] (
  [grades_sh_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(11) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [edp_code] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [dte] datetime  NOT NULL,
  [grade_1] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [grade_2] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [active_term] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[grades_sh] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for login_info
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[login_info]') AND type IN ('U'))
	DROP TABLE [dbo].[login_info]
GO

CREATE TABLE [dbo].[login_info] (
  [cinfo_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [last_name] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [first_name] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [mi] varchar(1) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [suffix] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [start_term] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [password] varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [dept] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [course_code] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [year] smallint  NULL,
  [sex] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [mobile_number] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [email] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [birthdate] datetime  NULL,
  [facebook] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [is_verified] smallint  NULL,
  [is_blocked] smallint  NULL,
  [allowed_units] smallint DEFAULT ((0)) NULL,
  [user_type] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [token] varchar(6) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [curr_year] smallint DEFAULT ((0)) NULL
)
GO

ALTER TABLE [dbo].[login_info] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for notification
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[notification]') AND type IN ('U'))
	DROP TABLE [dbo].[notification]
GO

CREATE TABLE [dbo].[notification] (
  [notif_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [notif_read] smallint  NOT NULL,
  [message] varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [dte] datetime  NOT NULL,
  [active_term] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[notification] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for oenrp
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[oenrp]') AND type IN ('U'))
	DROP TABLE [dbo].[oenrp]
GO

CREATE TABLE [dbo].[oenrp] (
  [oenrp_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [year_level] smallint  NOT NULL,
  [course_code] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [enrollment_date] datetime  NULL,
  [units] smallint  NOT NULL,
  [classification] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [dept] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [mdn] varchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [section] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [status] smallint  NOT NULL,
  [request_overload] smallint  NULL,
  [request_deblock] smallint  NULL,
  [request_promissory] smallint DEFAULT ((0)) NOT NULL,
  [promi_pay] int DEFAULT ((0)) NOT NULL,
  [registered_on] datetime  NULL,
  [submitted_on] datetime  NULL,
  [approved_reg_registrar] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [approved_reg_registrar_on] datetime  NULL,
  [disapproved_reg_registrar] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [disapproved_reg_registrar_on] datetime  NULL,
  [approved_reg_dean] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [approved_reg_dean_on] datetime  NULL,
  [disapproved_reg_dean] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [disapproved_reg_dean_on] datetime  NULL,
  [approved_dean] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [approved_dean_on] datetime  NULL,
  [disapproved_dean] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [disapproved_dean_on] datetime  NULL,
  [adjustment_count] smallint  NOT NULL,
  [adjustment_by] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [adjustment_on] datetime  NULL,
  [approved_acctg] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [approved_acctg_on] datetime  NULL,
  [needed_payment] int  NULL,
  [approved_cashier] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [approved_cashier_on] datetime  NULL,
  [active_term] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[oenrp] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for ostsp
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[ostsp]') AND type IN ('U'))
	DROP TABLE [dbo].[ostsp]
GO

CREATE TABLE [dbo].[ostsp] (
  [sts_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [edp_code] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [status] smallint  NOT NULL,
  [remarks] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [adjusted_on] datetime  NOT NULL,
  [active_term] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[ostsp] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for promissory
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[promissory]') AND type IN ('U'))
	DROP TABLE [dbo].[promissory]
GO

CREATE TABLE [dbo].[promissory] (
  [promi_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [promi_message] varchar(1500) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [promi_date] datetime  NOT NULL,
  [active_term] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[promissory] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for request_schedule
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[request_schedule]') AND type IN ('U'))
	DROP TABLE [dbo].[request_schedule]
GO

CREATE TABLE [dbo].[request_schedule] (
  [request_id] int  IDENTITY(1,1) NOT NULL,
  [subject_name] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [time_start] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [time_end] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [mdn] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [days] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [rtype] int  NULL,
  [m_time_start] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [m_time_end] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [status] int  NULL,
  [internal_code] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [size] int  NULL,
  [split_type] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [split_code] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [term] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [edp_code] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[request_schedule] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for requisite
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[requisite]') AND type IN ('U'))
	DROP TABLE [dbo].[requisite]
GO

CREATE TABLE [dbo].[requisite] (
  [requisite_id] int  IDENTITY(1,1) NOT NULL,
  [internal_code] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [requisite_code] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [requisite_type] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[requisite] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for schedules
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[schedules]') AND type IN ('U'))
	DROP TABLE [dbo].[schedules]
GO

CREATE TABLE [dbo].[schedules] (
  [schedule_id] int  IDENTITY(1,1) NOT NULL,
  [edp_code] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [description] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [internal_code] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [sub_type] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [units] smallint  NOT NULL,
  [time_start] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [time_end] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [mdn] varchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [days] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [m_time_start] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [m_time_end] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [m_days] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [size] int  NOT NULL,
  [pending_enrolled] int  NULL,
  [official_enrolled] int  NULL,
  [max_size] int  NOT NULL,
  [instructor] varchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [course_code] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [section] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [room] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [instructor_2] varchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [deployed] smallint  NOT NULL,
  [status] smallint  NOT NULL,
  [split_type] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [split_code] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [is_gened] smallint  NULL,
  [active_term] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[schedules] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for schedules_be
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[schedules_be]') AND type IN ('U'))
	DROP TABLE [dbo].[schedules_be]
GO

CREATE TABLE [dbo].[schedules_be] (
  [schedule_be_id] int  IDENTITY(1,1) NOT NULL,
  [edp_code] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [description] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [internal_code] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [sub_type] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [units] smallint  NOT NULL,
  [time_start] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [time_end] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [mdn] varchar(2) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [days] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [m_time_start] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [m_time_end] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [m_days] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [size] int  NOT NULL,
  [pending_enrolled] int  NULL,
  [official_enrolled] int  NULL,
  [max_size] int  NOT NULL,
  [instructor] varchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [course_code] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [section] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [room] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [instructor_2] varchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [deployed] smallint  NOT NULL,
  [status] smallint  NOT NULL,
  [split_type] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [split_code] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [is_gened] smallint  NULL,
  [active_term] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[schedules_be] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for school_info
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[school_info]') AND type IN ('U'))
	DROP TABLE [dbo].[school_info]
GO

CREATE TABLE [dbo].[school_info] (
  [school_info_id] int  IDENTITY(1,1) NOT NULL,
  [stud_info_id] int  NOT NULL,
  [elem_name] varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [elem_year] varchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [elem_last_year] smallint  NULL,
  [elem_type] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [elem_lrn_no] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [elem_esc_student_id] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [elem_esc_school_id] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [sec_name] varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [sec_year] varchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [sec_last_year] smallint  NULL,
  [sec_last_strand] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [sec_type] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [sec_lrn_no] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [sec_esc_student_id] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [sec_esc_school_id] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [col_name] varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [col_year] varchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [col_course] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [col_last_year] smallint  NULL,
  [active_term] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[school_info] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for student_info
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[student_info]') AND type IN ('U'))
	DROP TABLE [dbo].[student_info]
GO

CREATE TABLE [dbo].[student_info] (
  [stud_info_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [course_code] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [year_level] int  NOT NULL,
  [mdn] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [first_name] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [last_name] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [middle_name] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [suffix] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [gender] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [status] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [nationality] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [birth_date] datetime  NOT NULL,
  [birth_place] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [religion] varchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [date_created] datetime  NOT NULL,
  [date_updated] datetime  NOT NULL,
  [start_term] smallint  NOT NULL,
  [is_verified] smallint  NOT NULL,
  [token] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [classification] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [dept] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [active_term] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[student_info] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for student_request
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[student_request]') AND type IN ('U'))
	DROP TABLE [dbo].[student_request]
GO

CREATE TABLE [dbo].[student_request] (
  [stud_request_id] int  IDENTITY(1,1) NOT NULL,
  [stud_id] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [internal_code] nchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [term] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[student_request] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for subject_info
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[subject_info]') AND type IN ('U'))
	DROP TABLE [dbo].[subject_info]
GO

CREATE TABLE [dbo].[subject_info] (
  [sub_info_id] int  IDENTITY(1,1) NOT NULL,
  [internal_code] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [subject_name] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [subject_type] varchar(5) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [descr_1] varchar(250) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [descr_2] varchar(250) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [units] smallint DEFAULT ((0)) NOT NULL,
  [semester] smallint  NULL,
  [course_code] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [year_level] int  NULL,
  [split_type] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [split_code] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [curriculum_year] int  NULL
)
GO

ALTER TABLE [dbo].[subject_info] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for teacher_dept
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[teacher_dept]') AND type IN ('U'))
	DROP TABLE [dbo].[teacher_dept]
GO

CREATE TABLE [dbo].[teacher_dept] (
  [teacher_dept_id] int  IDENTITY(1,1) NOT NULL,
  [dept] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [id_number] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[teacher_dept] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for tmp_login
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[tmp_login]') AND type IN ('U'))
	DROP TABLE [dbo].[tmp_login]
GO

CREATE TABLE [dbo].[tmp_login] (
  [tmp_login_id] int  IDENTITY(1,1) NOT NULL,
  [email] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [token] varchar(6) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[tmp_login] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Auto increment value for 212assessment_be
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[212assessment_be]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table 212assessment_be
-- ----------------------------
ALTER TABLE [dbo].[212assessment_be] ADD CONSTRAINT [PK_212assessment_be] PRIMARY KEY CLUSTERED ([assess_be_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for 212assessment_cl
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[212assessment_cl]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table 212assessment_cl
-- ----------------------------
ALTER TABLE [dbo].[212assessment_cl] ADD CONSTRAINT [PK_212assessment_cl] PRIMARY KEY CLUSTERED ([assess_cl_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for 212assessment_sh
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[212assessment_sh]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table 212assessment_sh
-- ----------------------------
ALTER TABLE [dbo].[212assessment_sh] ADD CONSTRAINT [PK_212assessment_sh] PRIMARY KEY CLUSTERED ([assess_sh_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for 212attachments
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[212attachments]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table 212attachments
-- ----------------------------
ALTER TABLE [dbo].[212attachments] ADD CONSTRAINT [PK_212attachments] PRIMARY KEY CLUSTERED ([attachment_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for 212contact_address
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[212contact_address]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table 212contact_address
-- ----------------------------
ALTER TABLE [dbo].[212contact_address] ADD CONSTRAINT [PK_212contact_address] PRIMARY KEY CLUSTERED ([add_con_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for 212core_be
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[212core_be]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table 212core_be
-- ----------------------------
ALTER TABLE [dbo].[212core_be] ADD CONSTRAINT [PK_212core_be] PRIMARY KEY CLUSTERED ([core_be_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for 212core_sh
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[212core_sh]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table 212core_sh
-- ----------------------------
ALTER TABLE [dbo].[212core_sh] ADD CONSTRAINT [PK_212core_sh] PRIMARY KEY CLUSTERED ([core_sh_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for 212exam_promissory
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[212exam_promissory]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table 212exam_promissory
-- ----------------------------
ALTER TABLE [dbo].[212exam_promissory] ADD CONSTRAINT [PK_212ExamPromissory] PRIMARY KEY CLUSTERED ([exam_promi_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for 212family_info
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[212family_info]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table 212family_info
-- ----------------------------
ALTER TABLE [dbo].[212family_info] ADD CONSTRAINT [PK_212family_info] PRIMARY KEY CLUSTERED ([family_info_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for 212grades_be
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[212grades_be]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table 212grades_be
-- ----------------------------
ALTER TABLE [dbo].[212grades_be] ADD CONSTRAINT [PK_212grades_be] PRIMARY KEY CLUSTERED ([grades_be_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for 212grades_cl
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[212grades_cl]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table 212grades_cl
-- ----------------------------
ALTER TABLE [dbo].[212grades_cl] ADD CONSTRAINT [PK_212grades_cl] PRIMARY KEY CLUSTERED ([grades_cl_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for 212grades_sh
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[212grades_sh]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table 212grades_sh
-- ----------------------------
ALTER TABLE [dbo].[212grades_sh] ADD CONSTRAINT [PK_212grades_sh] PRIMARY KEY CLUSTERED ([grades_sh_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for 212oenrp
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[212oenrp]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table 212oenrp
-- ----------------------------
ALTER TABLE [dbo].[212oenrp] ADD CONSTRAINT [PK_212oenrp] PRIMARY KEY CLUSTERED ([oenrp_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for 212ostsp
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[212ostsp]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table 212ostsp
-- ----------------------------
ALTER TABLE [dbo].[212ostsp] ADD CONSTRAINT [PK_212ostsp] PRIMARY KEY CLUSTERED ([sts_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for 212promissory
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[212promissory]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table 212promissory
-- ----------------------------
ALTER TABLE [dbo].[212promissory] ADD CONSTRAINT [PK_212promissory] PRIMARY KEY CLUSTERED ([promi_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for 212schedules
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[212schedules]', RESEED, 4103)
GO


-- ----------------------------
-- Primary Key structure for table 212schedules
-- ----------------------------
ALTER TABLE [dbo].[212schedules] ADD CONSTRAINT [PK_212schedules] PRIMARY KEY CLUSTERED ([schedule_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for 212school_info
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[212school_info]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table 212school_info
-- ----------------------------
ALTER TABLE [dbo].[212school_info] ADD CONSTRAINT [PK_212school_info] PRIMARY KEY CLUSTERED ([school_info_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for 212student_info
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[212student_info]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table 212student_info
-- ----------------------------
ALTER TABLE [dbo].[212student_info] ADD CONSTRAINT [PK_212student_info] PRIMARY KEY CLUSTERED ([stud_info_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for adviser_section
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[adviser_section]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table adviser_section
-- ----------------------------
ALTER TABLE [dbo].[adviser_section] ADD CONSTRAINT [PK_adviser_section] PRIMARY KEY CLUSTERED ([section_ad_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for assessment_be
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[assessment_be]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table assessment_be
-- ----------------------------
ALTER TABLE [dbo].[assessment_be] ADD CONSTRAINT [PK_assessment_be] PRIMARY KEY CLUSTERED ([assess_be_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for assessment_cl
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[assessment_cl]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table assessment_cl
-- ----------------------------
ALTER TABLE [dbo].[assessment_cl] ADD CONSTRAINT [PK_assessment_cl] PRIMARY KEY CLUSTERED ([assess_cl_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for assessment_sh
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[assessment_sh]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table assessment_sh
-- ----------------------------
ALTER TABLE [dbo].[assessment_sh] ADD CONSTRAINT [PK_assessment_sh] PRIMARY KEY CLUSTERED ([assess_sh_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for attachments
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[attachments]', RESEED, 2004)
GO


-- ----------------------------
-- Primary Key structure for table attachments
-- ----------------------------
ALTER TABLE [dbo].[attachments] ADD CONSTRAINT [PK_attachments] PRIMARY KEY CLUSTERED ([attachment_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for config
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[config]', RESEED, 3)
GO


-- ----------------------------
-- Primary Key structure for table config
-- ----------------------------
ALTER TABLE [dbo].[config] ADD CONSTRAINT [PK_config] PRIMARY KEY CLUSTERED ([config_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for contact_address
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[contact_address]', RESEED, 2002)
GO


-- ----------------------------
-- Primary Key structure for table contact_address
-- ----------------------------
ALTER TABLE [dbo].[contact_address] ADD CONSTRAINT [PK_contact_address] PRIMARY KEY CLUSTERED ([add_con_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for core_be
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[core_be]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table core_be
-- ----------------------------
ALTER TABLE [dbo].[core_be] ADD CONSTRAINT [PK_core_be] PRIMARY KEY CLUSTERED ([core_be_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for core_sh
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[core_sh]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table core_sh
-- ----------------------------
ALTER TABLE [dbo].[core_sh] ADD CONSTRAINT [PK_core_sh] PRIMARY KEY CLUSTERED ([core_sh_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for course_list
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[course_list]', RESEED, 10)
GO


-- ----------------------------
-- Auto increment value for curriculum
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[curriculum]', RESEED, 1024)
GO


-- ----------------------------
-- Primary Key structure for table curriculum
-- ----------------------------
ALTER TABLE [dbo].[curriculum] ADD CONSTRAINT [PK_curriculum] PRIMARY KEY CLUSTERED ([curr_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for equivalence
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[equivalence]', RESEED, 19173)
GO


-- ----------------------------
-- Primary Key structure for table equivalence
-- ----------------------------
ALTER TABLE [dbo].[equivalence] ADD CONSTRAINT [PK_equivalence] PRIMARY KEY CLUSTERED ([equival_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for exam_promissory
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[exam_promissory]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table exam_promissory
-- ----------------------------
ALTER TABLE [dbo].[exam_promissory] ADD CONSTRAINT [PK_ExamPromissory] PRIMARY KEY CLUSTERED ([exam_promi_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for family_info
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[family_info]', RESEED, 2002)
GO


-- ----------------------------
-- Primary Key structure for table family_info
-- ----------------------------
ALTER TABLE [dbo].[family_info] ADD CONSTRAINT [PK_family_info] PRIMARY KEY CLUSTERED ([family_info_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for grade_evaluation
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[grade_evaluation]', RESEED, 15)
GO


-- ----------------------------
-- Auto increment value for grade_evaluation_be
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[grade_evaluation_be]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table grade_evaluation_be
-- ----------------------------
ALTER TABLE [dbo].[grade_evaluation_be] ADD CONSTRAINT [PK_grade_evaluation_be] PRIMARY KEY CLUSTERED ([grade_eval_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for grades_be
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[grades_be]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table grades_be
-- ----------------------------
ALTER TABLE [dbo].[grades_be] ADD CONSTRAINT [PK_grades_be] PRIMARY KEY CLUSTERED ([grades_be_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for grades_cl
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[grades_cl]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table grades_cl
-- ----------------------------
ALTER TABLE [dbo].[grades_cl] ADD CONSTRAINT [PK_grades_cl] PRIMARY KEY CLUSTERED ([grades_cl_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for grades_sh
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[grades_sh]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table grades_sh
-- ----------------------------
ALTER TABLE [dbo].[grades_sh] ADD CONSTRAINT [PK_grades_sh] PRIMARY KEY CLUSTERED ([grades_sh_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for login_info
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[login_info]', RESEED, 113008)
GO


-- ----------------------------
-- Primary Key structure for table login_info
-- ----------------------------
ALTER TABLE [dbo].[login_info] ADD CONSTRAINT [PK_login_info] PRIMARY KEY CLUSTERED ([cinfo_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for notification
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[notification]', RESEED, 4055)
GO


-- ----------------------------
-- Primary Key structure for table notification
-- ----------------------------
ALTER TABLE [dbo].[notification] ADD CONSTRAINT [PK_212notification] PRIMARY KEY CLUSTERED ([notif_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for oenrp
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[oenrp]', RESEED, 4022)
GO


-- ----------------------------
-- Primary Key structure for table oenrp
-- ----------------------------
ALTER TABLE [dbo].[oenrp] ADD CONSTRAINT [PK_oenrp] PRIMARY KEY CLUSTERED ([oenrp_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for ostsp
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[ostsp]', RESEED, 3015)
GO


-- ----------------------------
-- Primary Key structure for table ostsp
-- ----------------------------
ALTER TABLE [dbo].[ostsp] ADD CONSTRAINT [PK_ostsp] PRIMARY KEY CLUSTERED ([sts_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for promissory
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[promissory]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table promissory
-- ----------------------------
ALTER TABLE [dbo].[promissory] ADD CONSTRAINT [PK_promissory] PRIMARY KEY CLUSTERED ([promi_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for request_schedule
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[request_schedule]', RESEED, 1057)
GO


-- ----------------------------
-- Primary Key structure for table request_schedule
-- ----------------------------
ALTER TABLE [dbo].[request_schedule] ADD CONSTRAINT [PK_request_schedule] PRIMARY KEY CLUSTERED ([request_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for requisite
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[requisite]', RESEED, 3054)
GO


-- ----------------------------
-- Primary Key structure for table requisite
-- ----------------------------
ALTER TABLE [dbo].[requisite] ADD CONSTRAINT [PK_requisite] PRIMARY KEY CLUSTERED ([requisite_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for schedules
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[schedules]', RESEED, 2002)
GO


-- ----------------------------
-- Primary Key structure for table schedules
-- ----------------------------
ALTER TABLE [dbo].[schedules] ADD CONSTRAINT [PK_schedules] PRIMARY KEY CLUSTERED ([schedule_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for schedules_be
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[schedules_be]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table schedules_be
-- ----------------------------
ALTER TABLE [dbo].[schedules_be] ADD CONSTRAINT [PK_be_schedules] PRIMARY KEY CLUSTERED ([schedule_be_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for school_info
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[school_info]', RESEED, 2002)
GO


-- ----------------------------
-- Primary Key structure for table school_info
-- ----------------------------
ALTER TABLE [dbo].[school_info] ADD CONSTRAINT [PK_school_info] PRIMARY KEY CLUSTERED ([school_info_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for student_info
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[student_info]', RESEED, 2002)
GO


-- ----------------------------
-- Primary Key structure for table student_info
-- ----------------------------
ALTER TABLE [dbo].[student_info] ADD CONSTRAINT [PK_student_info] PRIMARY KEY CLUSTERED ([stud_info_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for student_request
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[student_request]', RESEED, 1115)
GO


-- ----------------------------
-- Primary Key structure for table student_request
-- ----------------------------
ALTER TABLE [dbo].[student_request] ADD CONSTRAINT [PK_student_request] PRIMARY KEY CLUSTERED ([stud_request_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for subject_info
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[subject_info]', RESEED, 4312)
GO


-- ----------------------------
-- Primary Key structure for table subject_info
-- ----------------------------
ALTER TABLE [dbo].[subject_info] ADD CONSTRAINT [PK_subject_info] PRIMARY KEY CLUSTERED ([sub_info_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for teacher_dept
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[teacher_dept]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table teacher_dept
-- ----------------------------
ALTER TABLE [dbo].[teacher_dept] ADD CONSTRAINT [PK_teacher_dept] PRIMARY KEY CLUSTERED ([teacher_dept_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for tmp_login
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[tmp_login]', RESEED, 2025)
GO


-- ----------------------------
-- Primary Key structure for table tmp_login
-- ----------------------------
ALTER TABLE [dbo].[tmp_login] ADD CONSTRAINT [PK_tmp_login] PRIMARY KEY CLUSTERED ([tmp_login_id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


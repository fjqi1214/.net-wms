-----------------------------added by leon.li 20130308----mail表添加手机发送状态-----------------------------------
alter table tblmail
add ISSENDTOPHONE varchar2(1) default 'N' not null;

----------------------------------------短信发送返回参数-----------------------------------------------

alter table tblmail
add PHONESENDRESULT varchar2(40) null;
-----------------------------------------end leon.li-------------------------------------------
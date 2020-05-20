<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
	<xsl:output method="html" indent="yes"/>
	<xsl:template match="/">
		<html>
			<head>
				<title>DNN Resource File Comparison</title>
				<style>
					* {font-family: Arial;}
					body{font-size:13px;}
					li {margin-top:5px}
					li p.file{font-weight:bold;margin-bottom:2px;padding:4px;}
					ol.new li p.file{background-color:#C2DCBE;}
					ol.modified li p.file{background-color:#C0D3DA;}
					ol.deleted li p.file{background-color:#E0BBBA;}

					span.new{background-color:#C2DCBE;padding:4px;width:200px;}
					span.modified{background-color:#C0D3DA;padding:4px;width:200px;}
					span.deleted{background-color:#E0BBBA;padding:4px;width:200px;}

					table{border:solid 1px #000;width:95%;margin-bottom:10px;font-size:12px}
					th{text-align:left;background-color:#aaa;color:#fff;}
					th.title{background-color:#888;color:#fff;border-bottom:solid 1px #dcdcdc;}
					th.t1{width:175px}
					th.t2{width:40%}
					th.t3{width:40%}
					td{border-bottom:solid 1px #dcdcdc;border-right:solid 1px #dcdcdc;}
				</style>
			</head>
			<body>
				<h1>DNN Resource File Comparison Report</h1>
				<xsl:apply-templates select="resourceCompare/summary"></xsl:apply-templates>
				<xsl:apply-templates select="resourceCompare/newFiles"></xsl:apply-templates>
				<xsl:apply-templates select="resourceCompare/modifiedFiles"></xsl:apply-templates>
				<xsl:apply-templates select="resourceCompare/deletedFiles"></xsl:apply-templates>
			</body>
		</html>
	</xsl:template>

	<xsl:template match="summary">
		<h2>Summary</h2>
		<ul>
			<li>
				<span class="new"><strong><a href="#new">Total New Files</a>: </strong><xsl:value-of select="totalNew/@files"/></span>
				<ul>
					<li>
						<strong>Total Keys: </strong><xsl:value-of select="totalNew/@keys"/>
					</li>
					<li>
						<strong>Total Words: </strong><xsl:value-of select="totalNew/@words"/>
					</li>
					<li>
						<strong>Files</strong>
						<ol>
							<xsl:for-each select="/resourceCompare/newFiles/file">
								<li>
									<a href="#{path}/{name}"><xsl:value-of select="path"/>\<xsl:value-of select="name"/></a>
								</li>
							</xsl:for-each>
						</ol>
					</li>
				</ul>
			</li>
			<li>
				<span class="modified"><strong><a href="#modified">Total Modified Files</a>: </strong><xsl:value-of select="totalModified/@files"/></span>
				<ul>
					<li>
						<strong>Total Keys: </strong><xsl:value-of select="totalModified/@keys"/>
					</li>
					<li>
						<strong>Total Words: </strong><xsl:value-of select="totalModified/@words"/>
					</li>
					<li>
						<strong>Files</strong>
						<ol>
							<xsl:for-each select="/resourceCompare/modifiedFiles/file">
								<li>
									<a href="#{path}/{name}">
										<xsl:value-of select="path"/>\<xsl:value-of select="name"/>
									</a>
								</li>
							</xsl:for-each>
						</ol>
					</li>
				</ul>
			</li>
			<li>
				<span class="deleted"><strong><a href="#deleted">Total Deleted Files</a>: </strong><xsl:value-of select="totalDeleted/@files"/></span>
				<ul>
					<li><strong>Files</strong>
						<ol>
							<xsl:for-each select="/resourceCompare/deletedFiles/file">
								<li>
									<a href="#{path}/{name}">
										<xsl:value-of select="path"/>\<xsl:value-of select="name"/>
									</a>
								</li>
							</xsl:for-each>
						</ol>
					</li>
				</ul>
			</li>
		</ul>
	</xsl:template>

	
	<xsl:template match="newFiles">
		<h2>
			<a name="#new">New Files</a>
		</h2>
		<ol class="new">
			<xsl:apply-templates select="file"></xsl:apply-templates>
		</ol>
	</xsl:template>
	<xsl:template match="newFiles/file">
		<li>
			<p class="file">
				<a name="#{path}/{name}">
					<xsl:value-of select="path"/>\<xsl:value-of select="name"/>
				</a>
			</p>
			<xsl:apply-templates select="newEntries"></xsl:apply-templates>
		</li>
	</xsl:template>


	<xsl:template match="deletedFiles">
		<h2>
			<a name="#deleted">Deleted Files</a>
		</h2>
		<ol class="deleted">
			<xsl:apply-templates select="file"></xsl:apply-templates>
		</ol>
	</xsl:template>
	<xsl:template match="deletedFiles/file">
		<li>
			<p class="file">
				<a name="#{path}/{name}">
					<xsl:value-of select="path"/>\<xsl:value-of select="name"/>
				</a>
			</p>
			<xsl:apply-templates select="deletedEntries"></xsl:apply-templates>
		</li>
	</xsl:template>


	<xsl:template match="modifiedFiles">
		<h2>
			<a name="#modified">Modified Files</a>
		</h2>
		<ol class="modified">
			<xsl:apply-templates select="file"></xsl:apply-templates>
		</ol>
	</xsl:template>
	<xsl:template match="modifiedFiles/file">
		<li>
			<p class="file">
				<a name="#{path}/{name}">
					<xsl:value-of select="path"/>\<xsl:value-of select="name"/>
				</a>
			</p>
			<xsl:apply-templates select="newEntries"></xsl:apply-templates>
			<xsl:apply-templates select="modifiedEntries"></xsl:apply-templates>
			<xsl:apply-templates select="deletedEntries"></xsl:apply-templates>
		</li>
	</xsl:template>


	<xsl:template match="newEntries">
		<table Width="100%" cellspacing="0" cellpadding="5">
			<tr>
				<th class="title" colspan="2">New Keys</th>
			</tr>
			<tr>
				<th>Key</th>
				<th>Value</th>
			</tr>
			<xsl:apply-templates select="key"><xsl:sort select="name" /></xsl:apply-templates>
		</table>
	</xsl:template>
	<xsl:template match="newEntries/key">
		<tr>
			<td><xsl:value-of select="name"/></td>
			<td><xsl:value-of select="newValue"/></td>
		</tr>
	</xsl:template>

	<xsl:template match="modifiedEntries">
		<table Width="100%" cellspacing="0" cellpadding="5">
			<tr>
				<th class="title" colspan="3">Modified Keys</th>
			</tr>
			<tr>
				<th class="t1">Key</th>
				<th class="t2">Old Value</th>
				<th class="t3">New Value</th>
			</tr>
			<xsl:apply-templates select="key"><xsl:sort select="name" /></xsl:apply-templates>
		</table>
	</xsl:template>
	<xsl:template match="modifiedEntries/key">
		<tr>
			<td><xsl:value-of select="name"/></td>
			<td><xsl:value-of select="oldValue"/></td>
			<td><xsl:value-of select="newValue"/></td>
		</tr>
	</xsl:template>

	<xsl:template match="deletedEntries">
		<table Width="100%" cellspacing="0" cellpadding="5">
			<tr>
				<th class="title" colspan="2">Deleted Keys</th>
			</tr>
			<tr>
				<th>Key</th>
				<th>Value</th>
			</tr>
			<xsl:apply-templates select="key"><xsl:sort select="name" /></xsl:apply-templates>
		</table>
	</xsl:template>
	<xsl:template match="deletedEntries/key">
		<tr>
			<td><xsl:value-of select="name"/></td>
			<td><xsl:value-of select="oldValue"/></td>
		</tr>
	</xsl:template>

</xsl:stylesheet>
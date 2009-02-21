<?xml version='1.0'?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">

<xsl:preserve-space elements="Step"/>

    <xsl:template match="/">
        <html>
            <head>
                <link href="../../style.css" rel="stylesheet" type="text/css"/>
            </head>
            <body>
                <div class="content">
                <table>
                    <tr>
                        <td>
                            <font size="4">
                                <b>Use Case:</b>
                            </font>
                        </td>
                        <td>
                            <font size="4">
                                <u><xsl:value-of select="UseCase/@Name"/></u>
                            </font>
                        </td>
                    </tr>

                    <tr height="10px">
                        <td></td>
                    </tr>

                    <tr>
                        <td>
                            <b>Context:</b>
                        </td>
                        <td>
                            <xsl:value-of select="//Context/@Description"/>
                        </td>
                    </tr>

                    <tr height="10px">
                        <td></td>
                    </tr>

                    <tr>
                        <td>
                            <b>Primary Actor:</b>
                        </td>
                        <td>
                            <xsl:value-of select="//PrimaryActor/@Name"/>
                        </td>
                    </tr>

                    <xsl:if test="count(//Status) > 0">
                    <tr>
                        <td>
                            <b>Status:</b>
                        </td>
                        <td>
                            <xsl:choose>
                                <xsl:when test="//Status[@Value='created']">Created <i>(this use case has been created as a placeholder)</i></xsl:when>
                                <xsl:when test="//Status[@Value='draft']">Draft <i>(the main success scenario is in place)</i></xsl:when>
                                <xsl:when test="//Status[@Value='complete']">Text Complete <i>(the extensions and notes are in place)</i></xsl:when>
                                <xsl:when test="//Status[@Value='implemented']">Implemented <i>(this use case has been implemented</i></xsl:when>
                                <xsl:when test="//Status[@Value='changed']">Changed <i>(the use case has changed since being implemented)</i></xsl:when>
                                <xsl:otherwise><i>Non-standard (<xsl:value-of select="//Level/@Name"/>)?</i></xsl:otherwise>
                            </xsl:choose>
                        </td>
                    </tr>
                    </xsl:if>

                    <tr>
                        <td>
                            <b>Level:</b>
                        </td>
                        <td>
                            <xsl:choose>
                                <xsl:when test="//Level[@Name='user-goal']">User-Goal</xsl:when>
                                <xsl:when test="//Level[@Name='summary']">Summary</xsl:when>
                                <xsl:when test="//Level[@Name='subfunction']">Sub-Function</xsl:when>
                                <xsl:otherwise><i>Non-standard (<xsl:value-of select="//Level/@Name"/>)?</i></xsl:otherwise>
                            </xsl:choose>
                        </td>
                    </tr>

                    <tr height="10px">
                        <td></td>
                    </tr>

                    <tr>
                        <td>
                            <b>Preconditions:</b>
                        </td>
                        <td>
                            <xsl:value-of select="//Preconditions/@Description"/>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <b>Trigger:</b>
                        </td>
                        <td>
                            <xsl:value-of select="//Trigger/@Description"/>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <b>Success Guarantee:</b>
                        </td>
                        <td>
                            <xsl:value-of select="//Guarantees/@Success"/>
                        </td>
                    </tr>

                    <tr>
                        <td WIDTH="140">
                            <b>Minimal Guarantee:</b>
                        </td>
                        <td>
                            <xsl:value-of select="//Guarantees/@Minimal"/>
                        </td>
                    </tr>
                </table>

                <p>
                    <br/>
                    <u>Main success scenario</u><br/>
                    <table>
                        <xsl:apply-templates select="/UseCase/MainSteps/Step"/>
                    </table>
                </p>

                <xsl:choose>
                    <xsl:when test="/UseCase/Extensions/Extension">
                        <p>
                            <br/>
                            <u><i>Extensions</i></u>
                            <xsl:apply-templates select="/UseCase/Extensions/Extension" />
                        </p>
                    </xsl:when>
                    <xsl:otherwise>
                        <p>
                            <br/>
                            <u><i>Extensions</i></u> (none)
                        </p>
                    </xsl:otherwise>
                </xsl:choose>

                <xsl:choose>
                    <xsl:when test="/UseCase/TechnologyDataVariations/Step">
                        <p>
                            <br/>
                            <u><i>Technology and Data Variations</i></u>
                            <table>
                                <xsl:apply-templates select="/UseCase/TechnologyDataVariations/Step" />
                            </table>
                        </p>
                    </xsl:when>
                    <xsl:otherwise>
                        <p>
                            <br/>
                            <u><i>Technology and Data Variations</i></u> (none)
                        </p>
                    </xsl:otherwise>
                </xsl:choose>
                
                <xsl:choose>
                    <xsl:when test="/UseCase/Notes/Note">
                        <p>
                            <br/>
                            <u><i>Notes</i></u>
                            <table>
                                <xsl:apply-templates select="/UseCase/Notes/Note" />
                            </table>
                        </p>
                    </xsl:when>
                    <xsl:otherwise>
                        <p>
                            <br/>
                            <u><i>Notes</i></u> (none)
                        </p>
                    </xsl:otherwise>
                </xsl:choose>
       
            </div>         
            </body>
        </html>
    </xsl:template>
    
    <xsl:template match="Extension" >
        <tr>
            <td></td>
            <td>
                <table>
                    <tr>
                        <td valign="top"><xsl:value-of select="@Id"/>.</td><td><xsl:value-of select="@Description"/>:</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <table>
                                <xsl:apply-templates />
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </xsl:template>

    <xsl:template match="Step">
        <tr>
            <td valign="top">
                <!-- increase width, or MS-Word wraps text! -->
                <xsl:attribute name="WIDTH">
                    <xsl:value-of select="(string-length(@Id) * string-length(@Id) * 1.5) + 20"/>
                </xsl:attribute>
                <xsl:value-of select="@Id"/>.
            </td>
            <td><xsl:apply-templates /></td>
        </tr>
    </xsl:template>

    <xsl:template match="Note">
        <tr>
            <td valign="top">
                <xsl:value-of select="@Id"/>.
            </td>
            <td><xsl:apply-templates /></td>
        </tr>
    </xsl:template>

    <xsl:template match="br">
        <br/><table></table>
    </xsl:template>

    <xsl:template match="ul">
        <ul>
            <xsl:apply-templates />
        </ul>
    </xsl:template>

    <xsl:template match="li">
        <li>
            <xsl:apply-templates />
        </li>
    </xsl:template>

    <xsl:template match="p">
        <p style="margin-top:0px;margin-bottom:5px;">
            <xsl:apply-templates />
        </p>
    </xsl:template>

    <xsl:template match="b">
        <b>
            <xsl:apply-templates />
        </b>
    </xsl:template>

    <xsl:template match="i">
        <i>
            <xsl:apply-templates />
        </i>
    </xsl:template>

    <xsl:template match="code">
        <code>
            <xsl:apply-templates />
        </code>
    </xsl:template>

    <xsl:template match="pre">
        <pre>
            <xsl:apply-templates />
        </pre>
    </xsl:template>

    <xsl:template match="tab">
        <span style="width:40px;"> test</span>
    </xsl:template>

    <xsl:template match="Call">
        <b><u><a>
            <xsl:attribute name="href">
                <xsl:value-of select="translate(@Name, ' ', '')"/>.xml
            </xsl:attribute>
            <xsl:value-of select="@Name"/>
        </a></u></b>
    </xsl:template>



</xsl:stylesheet>


// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:Legacy Shaders/VertexLit,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:34973,y:34415,varname:node_2865,prsc:2|normal-4424-RGB,custl-9801-OUT,olwid-4358-OUT,olcol-6388-OUT;n:type:ShaderForge.SFN_Tex2d,id:4952,x:32117,y:31919,cmnt:MainColor,varname:_MainTex,prsc:2,ntxv:0,isnm:False|UVIN-5723-UVOUT,TEX-9946-TEX;n:type:ShaderForge.SFN_LightVector,id:5704,x:31603,y:34664,cmnt:ライトベクトル,varname:node_5704,prsc:2;n:type:ShaderForge.SFN_Dot,id:2771,x:31800,y:34543,cmnt:法線とライトベクトルの内積,varname:node_2771,prsc:2,dt:4|A-4658-OUT,B-5704-OUT;n:type:ShaderForge.SFN_Slider,id:4789,x:31794,y:33267,ptovrint:False,ptlb:Mix_ShadowTexture,ptin:_Mix_ShadowTexture,varname:_Mix_ShadowTexture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.001,cur:0.001,max:0.999;n:type:ShaderForge.SFN_Vector1,id:2186,x:31722,y:33593,varname:node_2186,prsc:2,v1:0;n:type:ShaderForge.SFN_TexCoord,id:9651,x:32117,y:31712,varname:node_9651,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Slider,id:9036,x:31803,y:33944,ptovrint:False,ptlb:Mix_BaseTexture,ptin:_Mix_BaseTexture,varname:_Mix_BaseTexture,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.001,cur:0.001,max:0.999;n:type:ShaderForge.SFN_Tex2d,id:7699,x:32118,y:32275,cmnt:1影Color用,varname:node_7699,prsc:2,ntxv:0,isnm:False|UVIN-8522-UVOUT,TEX-9946-TEX;n:type:ShaderForge.SFN_NormalVector,id:7812,x:31366,y:34638,cmnt:法線ベクトル_ノーマルマップ含む,prsc:2,pt:True;n:type:ShaderForge.SFN_Color,id:9173,x:31165,y:36408,ptovrint:False,ptlb:Outline_Color,ptin:_Outline_Color,cmnt:アウトラインの色を設定,varname:_Outline_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.4980392,c2:0.4980392,c3:0.4980392,c4:1;n:type:ShaderForge.SFN_UVTile,id:5723,x:31737,y:31919,varname:node_5723,prsc:2|UVIN-1075-OUT,WDT-2723-OUT,HGT-1697-OUT,TILE-8592-OUT;n:type:ShaderForge.SFN_Vector1,id:2723,x:31484,y:32084,cmnt:UV分割数_横,varname:node_2723,prsc:2,v1:2;n:type:ShaderForge.SFN_Vector1,id:1697,x:31484,y:32167,cmnt:UV分割数_縦,varname:node_1697,prsc:2,v1:1;n:type:ShaderForge.SFN_UVTile,id:8522,x:31735,y:32277,varname:node_8522,prsc:2|UVIN-4131-OUT,WDT-2723-OUT,HGT-1697-OUT,TILE-5370-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:9946,x:31913,y:32102,ptovrint:False,ptlb:MainTexture,ptin:_MainTexture,cmnt:MainTexture,varname:_MainTexture,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_ViewPosition,id:4707,x:31432,y:37041,cmnt:カメラの現在の位置,varname:node_4707,prsc:2;n:type:ShaderForge.SFN_FragmentPosition,id:1834,x:31432,y:37197,cmnt:メッシュの現在のワールド座標,varname:node_1834,prsc:2;n:type:ShaderForge.SFN_Distance,id:5817,x:31663,y:37110,cmnt:AB間の距離,varname:node_5817,prsc:2|A-4707-XYZ,B-1834-XYZ;n:type:ShaderForge.SFN_LightColor,id:7952,x:34014,y:34875,cmnt:ライトカラー,varname:node_7952,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7142,x:34210,y:34655,cmnt:ライトカラーを乗算,varname:node_7142,prsc:2|A-3694-OUT,B-7952-RGB;n:type:ShaderForge.SFN_Slider,id:3047,x:31394,y:33535,ptovrint:False,ptlb:Shadow2nd,ptin:_Shadow2nd,cmnt:1影と2影の塗り分け段階を設定,varname:_Shadow2nd,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Lerp,id:9801,x:34601,y:34655,cmnt:texをみてグローカラーを混ぜる,varname:node_9801,prsc:2|A-7142-OUT,B-753-OUT,T-828-OUT;n:type:ShaderForge.SFN_Color,id:7491,x:31935,y:34181,ptovrint:False,ptlb:Glow_Color,ptin:_Glow_Color,cmnt:グローカラーを設定,varname:_Glow_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.4980392,c2:0.4980392,c3:0.4980392,c4:1;n:type:ShaderForge.SFN_Multiply,id:5742,x:32132,y:34235,cmnt:乗算して値をカンスト,varname:node_5742,prsc:2|A-7491-RGB,B-3686-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:753,x:34418,y:34901,ptovrint:False,ptlb:Is_Glow,ptin:_Is_Glow,cmnt:グローカラーのスイッチ,varname:_Is_Glow,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-7142-OUT,B-6255-OUT;n:type:ShaderForge.SFN_Color,id:4763,x:32036,y:35365,ptovrint:False,ptlb:Hilight_Color,ptin:_Hilight_Color,varname:_Hi_Light_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_HalfVector,id:6020,x:31951,y:35744,cmnt:ハーフベクトル,varname:node_6020,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:3461,x:31951,y:35563,cmnt:法線ベクトル,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:8100,x:32148,y:35636,cmnt:法線とハーフベクトルの内積,varname:node_8100,prsc:2,dt:0|A-3461-OUT,B-6020-OUT;n:type:ShaderForge.SFN_Step,id:7894,x:32154,y:35976,varname:node_7894,prsc:2|A-1840-OUT,B-6256-OUT;n:type:ShaderForge.SFN_Lerp,id:8918,x:32212,y:35272,varname:node_8918,prsc:2|A-7256-OUT,B-4763-RGB,T-8681-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:8681,x:31876,y:35313,ptovrint:False,ptlb:Is_Hilight,ptin:_Is_Hilight,varname:_Use_Hi_Light,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True|A-7025-OUT,B-551-OUT;n:type:ShaderForge.SFN_Vector1,id:7025,x:31691,y:35295,varname:node_7025,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:7256,x:32036,y:35272,varname:node_7256,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:5115,x:31722,y:33535,varname:node_5115,prsc:2,v1:1;n:type:ShaderForge.SFN_SwitchProperty,id:1247,x:31665,y:36408,ptovrint:False,ptlb:Is_BlendBaseColor,ptin:_Is_BlendBaseColor,cmnt:テクスチャカラーを反映するかのスイッチ,varname:_Is_BlendBaseColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-9173-RGB,B-3969-OUT;n:type:ShaderForge.SFN_Vector1,id:7395,x:31663,y:37489,varname:node_7395,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:1521,x:31663,y:37428,varname:set_Value1,prsc:2,v1:1;n:type:ShaderForge.SFN_Color,id:8560,x:32937,y:34695,ptovrint:False,ptlb:2nd_ShadeColor,ptin:_2nd_ShadeColor,cmnt:2影用カラーを設定,varname:_2nd_ShadeColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.4980392,c2:0.4980392,c3:0.4980392,c4:1;n:type:ShaderForge.SFN_Multiply,id:1881,x:33140,y:34695,cmnt:２影カラー,varname:node_1881,prsc:2|A-8560-RGB,B-185-OUT;n:type:ShaderForge.SFN_Lerp,id:9697,x:33366,y:34674,cmnt:範囲値で2影を混ぜる,varname:node_9697,prsc:2|A-185-OUT,B-1881-OUT,T-1375-OUT;n:type:ShaderForge.SFN_Slider,id:8830,x:31388,y:33837,ptovrint:False,ptlb:Shadow1st,ptin:_Shadow1st,cmnt:基本色と1影色の塗り分け段階を設定,varname:_Shadow1st,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_ValueProperty,id:9171,x:31663,y:37366,ptovrint:False,ptlb:Farthest_Distance,ptin:_Farthest_Distance,cmnt:最遠距離,varname:_Farthest_Distance,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:483,x:31663,y:37275,ptovrint:False,ptlb:Nearest_Distance,ptin:_Nearest_Distance,cmnt:最近接距離,varname:_Nearest_Distance,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Vector1,id:3686,x:31935,y:34345,varname:node_3686,prsc:2,v1:5;n:type:ShaderForge.SFN_Color,id:3822,x:31670,y:36593,ptovrint:False,ptlb:OutlineGlow_Color,ptin:_OutlineGlow_Color,cmnt:アウトラインのグローカラーを設定,varname:_Glow_Outline_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:4950,x:31869,y:36593,cmnt:グローを加算する,varname:node_4950,prsc:2|A-3822-RGB,B-9283-OUT;n:type:ShaderForge.SFN_Vector1,id:9283,x:31670,y:36749,varname:node_9283,prsc:2,v1:5;n:type:ShaderForge.SFN_SwitchProperty,id:6471,x:31868,y:36860,ptovrint:False,ptlb:Is_OutlineGlow,ptin:_Is_OutlineGlow,cmnt:アウトラインのスイッチ,varname:_Is_OutlineGlow,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-3631-OUT,B-1846-OUT;n:type:ShaderForge.SFN_Vector1,id:1846,x:31668,y:36906,varname:node_1846,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:3631,x:31668,y:36842,varname:node_3631,prsc:2,v1:0;n:type:ShaderForge.SFN_Multiply,id:382,x:32052,y:36593,cmnt:offなら0を乗算,varname:node_382,prsc:2|A-4950-OUT,B-6471-OUT;n:type:ShaderForge.SFN_Add,id:7976,x:32244,y:36407,cmnt:グローカラーを加算,varname:node_7976,prsc:2|A-1247-OUT,B-382-OUT;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:2925,x:31869,y:37275,cmnt:設定値を0から1で再マップ,varname:node_2925,prsc:2|IN-5817-OUT,IMIN-483-OUT,IMAX-9171-OUT,OMIN-1521-OUT,OMAX-7395-OUT;n:type:ShaderForge.SFN_Clamp01,id:2061,x:32054,y:37297,cmnt:0以上1以下の入力値を出力,varname:node_2061,prsc:2|IN-2925-OUT;n:type:ShaderForge.SFN_Multiply,id:285,x:32258,y:37325,cmnt:設定値を乗算,varname:node_285,prsc:2|A-2061-OUT,B-5048-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3449,x:31667,y:37601,ptovrint:False,ptlb:Outline_Width,ptin:_Outline_Width,cmnt:アウトラインの太さ,varname:_Outline_Width,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_RemapRange,id:5048,x:32049,y:37665,cmnt:100分の1で入力,varname:node_5048,prsc:2,frmn:0,frmx:1,tomn:0,tomx:0.01|IN-7218-OUT;n:type:ShaderForge.SFN_Multiply,id:8076,x:31165,y:36218,cmnt:同じtexを乗算して色味を調整,varname:node_8076,prsc:2|A-319-OUT,B-319-OUT;n:type:ShaderForge.SFN_Multiply,id:3969,x:31421,y:36470,cmnt:乗算texに指定色を乗算,varname:node_3969,prsc:2|A-8076-OUT,B-9173-RGB;n:type:ShaderForge.SFN_Set,id:8219,x:32389,y:34542,cmnt:光の当たり方,varname:set_HitLight,prsc:2|IN-6834-OUT;n:type:ShaderForge.SFN_Get,id:3924,x:31701,y:33430,cmnt:光の当たり方,varname:node_3924,prsc:2|IN-8219-OUT;n:type:ShaderForge.SFN_Get,id:6401,x:31701,y:33700,cmnt:光の当たり方,varname:node_6401,prsc:2|IN-8219-OUT;n:type:ShaderForge.SFN_Get,id:1075,x:31550,y:31919,varname:node_1075,prsc:2|IN-948-OUT;n:type:ShaderForge.SFN_Set,id:948,x:32367,y:31711,cmnt:UV座標値を0に設定,varname:set_UV0,prsc:2|IN-9651-UVOUT;n:type:ShaderForge.SFN_Get,id:4131,x:31550,y:32277,varname:node_4131,prsc:2|IN-948-OUT;n:type:ShaderForge.SFN_Set,id:9812,x:32367,y:31919,cmnt:ベースカラーtex,varname:set_txBase,prsc:2|IN-4952-RGB;n:type:ShaderForge.SFN_Get,id:319,x:30962,y:36218,cmnt:ベースカラーtex,varname:node_319,prsc:2|IN-9812-OUT;n:type:ShaderForge.SFN_Set,id:2487,x:32364,y:32785,cmnt:グローtex,varname:set_txGlow,prsc:2|IN-4127-RGB;n:type:ShaderForge.SFN_Set,id:7978,x:32370,y:32275,cmnt:1影カラーtex,varname:set_tx1stShadow,prsc:2|IN-7699-RGB;n:type:ShaderForge.SFN_Get,id:185,x:32916,y:34863,cmnt:1影カラーtex,varname:node_185,prsc:2|IN-7978-OUT;n:type:ShaderForge.SFN_Set,id:1847,x:32395,y:35634,cmnt:スペキュラ反射,varname:set_Specular,prsc:2|IN-8100-OUT;n:type:ShaderForge.SFN_Get,id:1840,x:31950,y:35946,cmnt:スペキュラ反射,varname:node_1840,prsc:2|IN-1847-OUT;n:type:ShaderForge.SFN_Set,id:8453,x:32401,y:35974,cmnt:ハイライトの範囲値,varname:set_HilightRange,prsc:2|IN-7894-OUT;n:type:ShaderForge.SFN_Set,id:6669,x:32433,y:37325,cmnt:アウトライン,varname:set_Outline,prsc:2|IN-285-OUT;n:type:ShaderForge.SFN_Set,id:7260,x:32414,y:36407,cmnt:アウトラインカラー,varname:set_OutlineColor,prsc:2|IN-7976-OUT;n:type:ShaderForge.SFN_Get,id:6388,x:34750,y:34806,cmnt:アウトラインカラー,varname:node_6388,prsc:2|IN-7260-OUT;n:type:ShaderForge.SFN_Get,id:4358,x:34750,y:34724,cmnt:アウトライン,varname:node_4358,prsc:2|IN-6669-OUT;n:type:ShaderForge.SFN_Get,id:828,x:34397,y:34709,cmnt:グローtex,varname:node_828,prsc:2|IN-2487-OUT;n:type:ShaderForge.SFN_Set,id:5889,x:32370,y:32429,cmnt:ハイライトtex,varname:set_txHilight,prsc:2|IN-8578-RGB;n:type:ShaderForge.SFN_Get,id:551,x:31670,y:35350,varname:node_551,prsc:2|IN-5889-OUT;n:type:ShaderForge.SFN_Set,id:9757,x:32404,y:35272,cmnt:ハイライトの値,varname:set_HilightValue,prsc:2|IN-8918-OUT;n:type:ShaderForge.SFN_Set,id:3587,x:32377,y:33700,cmnt:1影の範囲値,varname:set_Step1st,prsc:2|IN-4944-OUT;n:type:ShaderForge.SFN_Set,id:2345,x:32371,y:33430,cmnt:2影の範囲値,varname:set_Step2nd,prsc:2|IN-3750-OUT;n:type:ShaderForge.SFN_Get,id:1375,x:33119,y:34863,cmnt:2影の範囲値,varname:node_1375,prsc:2|IN-2345-OUT;n:type:ShaderForge.SFN_Set,id:5283,x:32373,y:34235,cmnt:グローカラー,varname:set_GlowColor,prsc:2|IN-5742-OUT;n:type:ShaderForge.SFN_Get,id:6255,x:34231,y:34921,cmnt:グローカラー,varname:node_6255,prsc:2|IN-5283-OUT;n:type:ShaderForge.SFN_UVTile,id:5289,x:31742,y:32429,varname:node_5289,prsc:2|UVIN-8992-OUT,WDT-3326-OUT,HGT-1818-OUT,TILE-2905-OUT;n:type:ShaderForge.SFN_Vector1,id:3326,x:31489,y:32594,cmnt:UV分割数_横,varname:node_3326,prsc:2,v1:2;n:type:ShaderForge.SFN_Vector1,id:1818,x:31489,y:32677,cmnt:UV分割数_縦,varname:node_1818,prsc:2,v1:1;n:type:ShaderForge.SFN_UVTile,id:8862,x:31740,y:32787,varname:node_8862,prsc:2|UVIN-7644-OUT,WDT-3326-OUT,HGT-1818-OUT,TILE-8921-OUT;n:type:ShaderForge.SFN_Get,id:8992,x:31555,y:32429,varname:node_8992,prsc:2|IN-948-OUT;n:type:ShaderForge.SFN_Get,id:7644,x:31555,y:32787,varname:node_7644,prsc:2|IN-948-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:9233,x:31922,y:32580,ptovrint:False,ptlb:MaskTexture,ptin:_MaskTexture,varname:_MaskTexture,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:8578,x:32118,y:32429,cmnt:ハイライトマスク,varname:node_8578,prsc:2,ntxv:0,isnm:False|UVIN-5289-UVOUT,TEX-9233-TEX;n:type:ShaderForge.SFN_Tex2d,id:4127,x:32123,y:32785,cmnt:グローマスク,varname:node_4127,prsc:2,ntxv:0,isnm:False|UVIN-8862-UVOUT,TEX-9233-TEX;n:type:ShaderForge.SFN_ValueProperty,id:8592,x:31571,y:32002,ptovrint:False,ptlb:tileID_BaseColor,ptin:_tileID_BaseColor,varname:node_8592,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_ValueProperty,id:5370,x:31576,y:32359,ptovrint:False,ptlb:tileID_1stShadow,ptin:_tileID_1stShadow,varname:_tileID_Base_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:2905,x:31576,y:32512,ptovrint:False,ptlb:tileID_Hilight,ptin:_tileID_Hilight,varname:_tileID_1stShadow_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_ValueProperty,id:8921,x:31576,y:32872,ptovrint:False,ptlb:tileID_Glow,ptin:_tileID_Glow,varname:_tileID_Hilight_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Slider,id:6628,x:31445,y:35165,ptovrint:False,ptlb:Tweak_SystemShadows,ptin:_Tweak_SystemShadows,varname:node_6628,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:6834,x:32222,y:34542,varname:node_6834,prsc:2|A-2771-OUT,B-5318-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:4016,x:31218,y:34861,varname:node_4016,prsc:2;n:type:ShaderForge.SFN_Vector1,id:2921,x:31422,y:35061,varname:node_2921,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:3917,x:31422,y:34861,varname:node_3917,prsc:2|A-4016-OUT,B-2921-OUT;n:type:ShaderForge.SFN_Add,id:7756,x:31603,y:34861,varname:node_7756,prsc:2|A-3917-OUT,B-2921-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:5318,x:32025,y:34840,ptovrint:False,ptlb:Is_SystemShadows,ptin:_Is_SystemShadows,varname:node_5318,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True|A-2771-OUT,B-7966-OUT;n:type:ShaderForge.SFN_Get,id:3983,x:33345,y:34583,cmnt:ベースカラーtex,varname:node_3983,prsc:2|IN-9812-OUT;n:type:ShaderForge.SFN_Lerp,id:443,x:33590,y:34655,varname:node_443,prsc:2|A-3983-OUT,B-9697-OUT,T-3131-OUT;n:type:ShaderForge.SFN_Get,id:3131,x:33345,y:34863,cmnt:1影の範囲値,varname:node_3131,prsc:2|IN-3587-OUT;n:type:ShaderForge.SFN_Blend,id:6017,x:33804,y:34655,varname:node_6017,prsc:2,blmd:6,clmp:True|SRC-443-OUT,DST-5988-OUT;n:type:ShaderForge.SFN_Get,id:5988,x:33569,y:34863,cmnt:ハイライトの値,varname:node_5988,prsc:2|IN-9757-OUT;n:type:ShaderForge.SFN_Get,id:1640,x:33783,y:34861,cmnt:ハイライトの範囲値,varname:node_1640,prsc:2|IN-8453-OUT;n:type:ShaderForge.SFN_Lerp,id:3694,x:34014,y:34655,varname:node_3694,prsc:2|A-6017-OUT,B-443-OUT,T-1640-OUT;n:type:ShaderForge.SFN_Multiply,id:7966,x:31814,y:34861,varname:node_7966,prsc:2|A-7756-OUT,B-6628-OUT;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:3348,x:31256,y:33626,varname:node_3348,prsc:2|IN-3047-OUT,IMIN-2556-OUT,IMAX-3464-OUT,OMIN-2556-OUT,OMAX-8830-OUT;n:type:ShaderForge.SFN_Vector1,id:2556,x:31028,y:33686,varname:node_2556,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:3464,x:31028,y:33748,varname:node_3464,prsc:2,v1:1;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:2335,x:31962,y:33700,varname:node_2335,prsc:2|IN-6401-OUT,IMIN-3360-OUT,IMAX-8830-OUT,OMIN-5115-OUT,OMAX-2186-OUT;n:type:ShaderForge.SFN_Clamp01,id:4944,x:32138,y:33700,cmnt:0以上1以下の入力値を出力,varname:node_4944,prsc:2|IN-2335-OUT;n:type:ShaderForge.SFN_Subtract,id:3360,x:32138,y:33924,varname:node_3360,prsc:2|A-8830-OUT,B-9036-OUT;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:8324,x:31951,y:33430,varname:node_8324,prsc:2|IN-3924-OUT,IMIN-6885-OUT,IMAX-3348-OUT,OMIN-5115-OUT,OMAX-2186-OUT;n:type:ShaderForge.SFN_Clamp01,id:3750,x:32127,y:33430,cmnt:0以上1以下の入力値を出力,varname:node_3750,prsc:2|IN-8324-OUT;n:type:ShaderForge.SFN_Subtract,id:6885,x:32127,y:33247,varname:node_6885,prsc:2|A-3348-OUT,B-4789-OUT;n:type:ShaderForge.SFN_Tex2d,id:9130,x:32123,y:32957,ptovrint:False,ptlb:OutlineSampler,ptin:_OutlineSampler,cmnt:アウトラインマスク,varname:node_9130,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-9016-OUT;n:type:ShaderForge.SFN_Set,id:3394,x:32364,y:32977,cmnt:アウトラインtex,varname:set_txOutline,prsc:2|IN-9130-RGB;n:type:ShaderForge.SFN_Get,id:9016,x:31895,y:32957,varname:node_9016,prsc:2|IN-948-OUT;n:type:ShaderForge.SFN_Tex2d,id:4424,x:34771,y:34447,ptovrint:False,ptlb:NormalMap,ptin:_NormalMap,varname:node_4424,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True|UVIN-4778-OUT;n:type:ShaderForge.SFN_Get,id:4778,x:34586,y:34447,varname:node_4778,prsc:2|IN-948-OUT;n:type:ShaderForge.SFN_Get,id:4185,x:31646,y:37686,cmnt:アウトラインtex,varname:node_4185,prsc:2|IN-3394-OUT;n:type:ShaderForge.SFN_Multiply,id:7218,x:31864,y:37665,varname:node_7218,prsc:2|A-3449-OUT,B-4185-OUT;n:type:ShaderForge.SFN_Vector1,id:6256,x:31971,y:36010,varname:node_6256,prsc:2,v1:0;n:type:ShaderForge.SFN_NormalVector,id:4381,x:31366,y:34467,cmnt:法線ベクトル,prsc:2,pt:False;n:type:ShaderForge.SFN_SwitchProperty,id:4658,x:31603,y:34467,ptovrint:False,ptlb:Is_Normal,ptin:_Is_Normal,varname:node_4658,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-4381-OUT,B-7812-OUT;n:type:ShaderForge.SFN_NormalVector,id:10,x:31858,y:34726,cmnt:法線ベクトル,prsc:2,pt:True;proporder:9946-8592-5370-9233-2905-8921-8560-5318-6628-9036-4789-8830-3047-4658-4424-9130-1247-3449-9173-9171-483-6471-3822-753-7491-8681-4763;pass:END;sub:END;*/

Shader "Grenge/PJ_colorful/Toon_hi_2x2" {
    Properties {
		_ChangeBaseColor ("ChangeBaseColor", Color) = (0, 0, 0, 0)
		_ChangeShadowColor ("ChangeShadowColor", Color) = (0, 0, 0, 0)

        _MainTexture ("MainTexture", 2D) = "white" {}
        _tileID_BaseColor ("tileID_BaseColor", Float ) = 2
        _tileID_1stShadow ("tileID_1stShadow", Float ) = 1
        _MaskTexture ("MaskTexture", 2D) = "white" {}
        _tileID_Hilight ("tileID_Hilight", Float ) = 2
        _tileID_Glow ("tileID_Glow", Float ) = 1
        [HideInInspector]_2nd_ShadeColor ("2nd_ShadeColor", Color) = (0.4980392,0.4980392,0.4980392,1)
        [HideInInspector][MaterialToggle] _Is_SystemShadows ("Is_SystemShadows", Float ) = 0.5
        [HideInInspector]_Tweak_SystemShadows ("Tweak_SystemShadows", Range(0, 1)) = 1
        [HideInInspector]_Mix_BaseTexture ("Mix_BaseTexture", Range(0.001, 0.999)) = 0.001
        [HideInInspector]_Mix_ShadowTexture ("Mix_ShadowTexture", Range(0.001, 0.999)) = 0.001
        _Shadow1st ("Shadow1st", Range(0, 1)) = 0
        [HideInInspector]_Shadow2nd ("Shadow2nd", Range(0, 1)) = 0
        [MaterialToggle] _Is_Normal ("Is_Normal", Float ) = 0
        _NormalMap ("NormalMap", 2D) = "bump" {}
        _OutlineSampler ("OutlineSampler", 2D) = "white" {}
        [HideInInspector][MaterialToggle] _Is_BlendBaseColor ("Is_BlendBaseColor", Float ) = 0.4980392
        _Outline_Width ("Outline_Width", Float ) = 0
        _Outline_Color ("Outline_Color", Color) = (0.4980392,0.4980392,0.4980392,1)
        [HideInInspector]_Farthest_Distance ("Farthest_Distance", Float ) = 50
        [HideInInspector]_Nearest_Distance ("Nearest_Distance", Float ) = 0
        [HideInInspector][MaterialToggle] _Is_OutlineGlow ("Is_OutlineGlow", Float ) = 0
        [HideInInspector]_OutlineGlow_Color ("OutlineGlow_Color", Color) = (0.5,0.5,0.5,1)
        [MaterialToggle] _Is_Glow ("Is_Glow", Float ) = 0
        _Glow_Color ("Glow_Color", Color) = (0.4980392,0.4980392,0.4980392,1)
        [MaterialToggle] _Is_Hilight ("Is_Hilight", Float ) = 0
        _Hilight_Color ("Hilight_Color", Color) = (1,1,1,1)

		[Space(15)]
		_Brightness ("Brightness", Range(0.0, 1.0)) = 1.0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal ps4 n3ds wiiu 
            #pragma target 3.0
            uniform float _Mix_ShadowTexture;
            uniform float _Mix_BaseTexture;
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform float _Shadow2nd;
            uniform float4 _Glow_Color;
            uniform fixed _Is_Glow;
            uniform float4 _Hilight_Color;
            uniform fixed _Is_Hilight;
            uniform float4 _2nd_ShadeColor;
            uniform float _Shadow1st;
            uniform sampler2D _MaskTexture; uniform float4 _MaskTexture_ST;
            uniform float _tileID_BaseColor;
            uniform float _tileID_1stShadow;
            uniform float _tileID_Hilight;
            uniform float _tileID_Glow;
            uniform float _Tweak_SystemShadows;
            uniform fixed _Is_SystemShadows;
			uniform float _Brightness;
			uniform float4 _ChangeBaseColor;
			uniform float4 _ChangeShadowColor;
			uniform float	_LightColorWeight;
			uniform float3	_ToonLightColor;
			uniform float3	_ToonLightDir;
			uniform float	_ToonLightWeight;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform fixed _Is_Normal;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float2 set_UV0 = i.uv0; // UV座標値を0に設定
                float node_7256 = 0.0;
                float node_3326 = 2.0; // UV分割数_横
                float node_1818 = 1.0; // UV分割数_縦
                float2 node_5289_tc_rcp = float2(1.0,1.0)/float2( node_3326, node_1818 );
                float node_5289_ty = floor(_tileID_Hilight * node_5289_tc_rcp.x);
                float node_5289_tx = _tileID_Hilight - node_3326 * node_5289_ty;
                float2 node_5289 = (set_UV0 + float2(node_5289_tx, node_5289_ty)) * node_5289_tc_rcp;
                float4 node_8578 = tex2D(_MaskTexture,TRANSFORM_TEX(node_5289, _MaskTexture)); // ハイライトマスク
                float3 set_txHilight = node_8578.rrr; // ハイライトtex
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float2 node_4778 = set_UV0;
                float3 _NormalMap_var = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(node_4778, _NormalMap)));
                float3 normalLocal = _NormalMap_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float node_2723 = 2.0; // UV分割数_横
                float node_1697 = 1.0; // UV分割数_縦
                float2 node_5723_tc_rcp = float2(1.0,1.0)/float2( node_2723, node_1697 );
                float node_5723_ty = floor(_tileID_BaseColor * node_5723_tc_rcp.x);
                float node_5723_tx = _tileID_BaseColor - node_2723 * node_5723_ty;
                float2 node_5723 = (set_UV0 + float2(node_5723_tx, node_5723_ty)) * node_5723_tc_rcp;
                float4 _MainTex = tex2D(_MainTexture,TRANSFORM_TEX(node_5723, _MainTexture)); // MainColor
                float3 set_txBase = _MainTex.rgb; // ベースカラーtex
                float2 node_8522_tc_rcp = float2(1.0,1.0)/float2( node_2723, node_1697 );
                float node_8522_ty = floor(_tileID_1stShadow * node_8522_tc_rcp.x);
                float node_8522_tx = _tileID_1stShadow - node_2723 * node_8522_ty;
                float2 node_8522 = (set_UV0 + float2(node_8522_tx, node_8522_ty)) * node_8522_tc_rcp;
                float4 node_7699 = tex2D(_MainTexture,TRANSFORM_TEX(node_8522, _MainTexture)); // 1影Color用
                float3 set_tx1stShadow = node_7699.rgb; // 1影カラーtex				// 色マージ
				set_txBase      = lerp(set_txBase,      _ChangeBaseColor.rgb,   node_8578.ggg);
				set_tx1stShadow = lerp(set_tx1stShadow, _ChangeShadowColor.rgb, node_8578.ggg);

				// 影響度を加味した演出用ライト色(100%反映にすると色味が強く出過ぎるため、最大50%に抑える)
				float3 toon_light_color = _ToonLightColor * min(_ToonLightWeight, 0.5);
				// 影への乗算カラーの反映率。1に近いほど光による影の濃さが増す
				float shade_darkness_weight = 0.6;
				// 影への乗算カラー
				float3 set_tx1stShadowMulti = (
					1 - (toon_light_color.g + toon_light_color.b) * shade_darkness_weight,
					1 - (toon_light_color.r + toon_light_color.b) * shade_darkness_weight,
					1 - (toon_light_color.r + toon_light_color.g) * shade_darkness_weight
				);
				set_txBase += toon_light_color.rgb;
				set_tx1stShadow *= set_tx1stShadowMulti;
				
                float node_2771 = 0.5*dot(lerp( i.normalDir, normalDirection, _Is_Normal ),lightDirection)+0.5; // 法線とライトベクトルの内積
                float node_2921 = 0.5;
                float set_HitLight = (node_2771*lerp( node_2771, (((attenuation*node_2921)+node_2921)*_Tweak_SystemShadows), _Is_SystemShadows )); // 光の当たり方
                float node_2556 = 0.0;
                float node_3348 = (node_2556 + ( (_Shadow2nd - node_2556) * (_Shadow1st - node_2556) ) / (1.0 - node_2556));
                float node_6885 = (node_3348-_Mix_ShadowTexture);
                float node_5115 = 1.0;
                float node_2186 = 0.0;
                float set_Step2nd = saturate((node_5115 + ( (set_HitLight - node_6885) * (node_2186 - node_5115) ) / (node_3348 - node_6885))); // 2影の範囲値
                float node_3360 = (_Shadow1st-_Mix_BaseTexture);
                float set_Step1st = saturate((node_5115 + ( (set_HitLight - node_3360) * (node_2186 - node_5115) ) / (_Shadow1st - node_3360))); // 1影の範囲値
                float3 node_443 = lerp(set_txBase,lerp(set_tx1stShadow,(_2nd_ShadeColor.rgb*set_tx1stShadow),set_Step2nd),set_Step1st);
                float3 set_HilightValue = lerp(float3(node_7256,node_7256,node_7256),_Hilight_Color.rgb,lerp( 0.0, set_txHilight, _Is_Hilight )); // ハイライトの値
                float set_Specular = dot(i.normalDir,halfDirection); // スペキュラ反射
                float set_HilightRange = step(set_Specular,0.0); // ハイライトの範囲値
                float3 node_7142 = (lerp(saturate((1.0-(1.0-node_443)*(1.0-set_HilightValue))),node_443,set_HilightRange)*lerp(float3(1, 1, 1), _LightColor0, _LightColorWeight)); // ライトカラーを乗算
                float3 set_GlowColor = (_Glow_Color.rgb*5.0); // グローカラー
                float2 node_8862_tc_rcp = float2(1.0,1.0)/float2( node_3326, node_1818 );
                float node_8862_ty = floor(_tileID_Glow * node_8862_tc_rcp.x);
                float node_8862_tx = _tileID_Glow - node_3326 * node_8862_ty;
                float2 node_8862 = (set_UV0 + float2(node_8862_tx, node_8862_ty)) * node_8862_tc_rcp;
                float4 node_4127 = tex2D(_MaskTexture,TRANSFORM_TEX(node_8862, _MaskTexture)); // グローマスク
                float3 set_txGlow = node_4127.rrr; // グローtex
                float3 finalColor = lerp(node_7142,lerp( node_7142, set_GlowColor, _Is_Glow ),set_txGlow);
                fixed4 finalRGBA = fixed4(lerp(0.0.xxx, finalColor, _Brightness),1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
		Pass {
            Name "Outline"
            Tags {
            }
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal ps4 n3ds wiiu 
            #pragma target 3.0
            uniform float4 _Outline_Color;
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform fixed _Is_BlendBaseColor;
            uniform float _Farthest_Distance;
            uniform float _Nearest_Distance;
            uniform float4 _OutlineGlow_Color;
            uniform fixed _Is_OutlineGlow;
            uniform float _Outline_Width;
            uniform float _tileID_BaseColor;
			uniform float _Brightness;
            uniform sampler2D _OutlineSampler; uniform float4 _OutlineSampler_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float set_Value1 = 1.0;
                float2 set_UV0 = o.uv0; // UV座標値を0に設定
                float2 node_9016 = set_UV0;
                float4 _OutlineSampler_var = tex2Dlod(_OutlineSampler,float4(TRANSFORM_TEX(node_9016, _OutlineSampler),0.0,0)); // アウトラインマスク
                float3 set_txOutline = _OutlineSampler_var.rgb; // アウトラインtex
                float3 set_Outline = (saturate((set_Value1 + ( (distance(_WorldSpaceCameraPos,mul(unity_ObjectToWorld, v.vertex).rgb) - _Nearest_Distance) * (0.0 - set_Value1) ) / (_Farthest_Distance - _Nearest_Distance)))*((_Outline_Width*set_txOutline)*0.01+0.0)); // アウトライン
                o.pos = UnityObjectToClipPos( float4(v.vertex.xyz + v.normal*set_Outline,1) );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float node_2723 = 2.0; // UV分割数_横
                float node_1697 = 1.0; // UV分割数_縦
                float2 node_5723_tc_rcp = float2(1.0,1.0)/float2( node_2723, node_1697 );
                float node_5723_ty = floor(_tileID_BaseColor * node_5723_tc_rcp.x);
                float node_5723_tx = _tileID_BaseColor - node_2723 * node_5723_ty;
                float2 set_UV0 = i.uv0; // UV座標値を0に設定
                float2 node_5723 = (set_UV0 + float2(node_5723_tx, node_5723_ty)) * node_5723_tc_rcp;
                float4 _MainTex = tex2D(_MainTexture,TRANSFORM_TEX(node_5723, _MainTexture)); // MainColor
                float3 set_txBase = _MainTex.rgb; // ベースカラーtex
                float3 node_319 = set_txBase; // ベースカラーtex
                float3 set_OutlineColor = (lerp( _Outline_Color.rgb, ((node_319*node_319)*_Outline_Color.rgb), _Is_BlendBaseColor )+((_OutlineGlow_Color.rgb*5.0)*lerp( 0.0, 1.0, _Is_OutlineGlow ))); // アウトラインカラー
				return fixed4(lerp(0.0.xxx, set_OutlineColor, _Brightness),1.0);
            }
            ENDCG
        }
    }
	FallBack "Mobile/VertexLit"
    CustomEditor "ShaderForgeMaterialInspector"
}

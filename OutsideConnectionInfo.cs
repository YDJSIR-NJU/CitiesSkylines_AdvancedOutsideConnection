﻿using ColossalFramework.UI;
using UnityEngine;

namespace AdvancedOutsideConnection
{
    class OutsideConnectionInfo : UICustomControl
    {
        private ushort m_BuildingID = 0;
        private UIPanel m_MainPanel = null;
        private UIPanel m_Background = null;
        private UISprite m_TransportTypeSprite = null;
        private UITextField m_ConnectionNameTextfield = null;
        private Color32 m_BackgroundColor = new Color32(49, 52, 58, 0);
        private UIButton m_DetailViewButton = null;
        private bool m_IsMouseOver = false;
        private OutsideConnectionSettings m_CachedSettings = null;
        public OutsideConnectionSettings currentSettings => m_CachedSettings;

        private void Awake()
        {
            m_MainPanel = gameObject.AddComponent<UIPanel>();
            m_MainPanel.name = "MainPanel";
            m_MainPanel.size = new Vector2(400, 45);

            m_Background = m_MainPanel.AddUIComponent<UIPanel>();
            m_Background.name = "BackgroundPanel";
            m_Background.backgroundSprite = "InfoviewPanel";
            m_Background.position = Vector3.zero;
            m_Background.size = m_MainPanel.size;
            m_Background.anchor = UIAnchorStyle.All;

            m_TransportTypeSprite = m_MainPanel.AddUIComponent<UISprite>();
            m_TransportTypeSprite.anchor = UIAnchorStyle.Left | UIAnchorStyle.CenterVertical;
            m_TransportTypeSprite.size = new Vector2(28, 28);
            m_TransportTypeSprite.relativePosition = new Vector3(12, 8);

            m_ConnectionNameTextfield = m_MainPanel.AddUIComponent<UITextField>();
            m_ConnectionNameTextfield.size = new Vector2(100, 35);
            m_ConnectionNameTextfield.relativePosition = new Vector3(100, 10);
            m_ConnectionNameTextfield.horizontalAlignment = UIHorizontalAlignment.Center;
            m_ConnectionNameTextfield.verticalAlignment = UIVerticalAlignment.Middle;
            m_ConnectionNameTextfield.anchor = UIAnchorStyle.Right | UIAnchorStyle.Left | UIAnchorStyle.CenterVertical;
            m_ConnectionNameTextfield.textScale = 1f;
            m_ConnectionNameTextfield.font = WidgetsFactory.instance.textFont;
            m_ConnectionNameTextfield.textColor = new Color32(185, 221, 254, 255);
            m_ConnectionNameTextfield.multiline = false;
            m_ConnectionNameTextfield.readOnly = false;
            m_ConnectionNameTextfield.cursorBlinkTime = 0.45f;
            m_ConnectionNameTextfield.cursorWidth = 1;
            m_ConnectionNameTextfield.selectionSprite = "EmptySprite";
            m_ConnectionNameTextfield.focusedBgSprite = "TextFieldPanel";
            m_ConnectionNameTextfield.hoveredBgSprite = "TextFieldPanelHovered";
            m_ConnectionNameTextfield.selectionBackgroundColor = Color.white;
            m_ConnectionNameTextfield.allowFloats = false;
            m_ConnectionNameTextfield.numericalOnly = false;
            m_ConnectionNameTextfield.allowNegative = false;
            m_ConnectionNameTextfield.isPasswordField = false;
            m_ConnectionNameTextfield.builtinKeyNavigation = true;
            m_ConnectionNameTextfield.padding = new RectOffset(8, 3, 5, 5);
            m_ConnectionNameTextfield.eventTextSubmitted += delegate (UIComponent component, string newText)
            {
                if (m_CachedSettings != null)
                    m_CachedSettings.Name = newText;
            };

            m_DetailViewButton = m_MainPanel.AddUIComponent<UIButton>();
            m_DetailViewButton.size = new Vector2(28, 28);
            m_DetailViewButton.relativePosition = new Vector3(m_MainPanel.width - 8 - m_DetailViewButton.width, 8);
            m_DetailViewButton.anchor = UIAnchorStyle.Right | UIAnchorStyle.CenterVertical;
            
            m_DetailViewButton.normalBgSprite = "LineDetailButton";
            m_DetailViewButton.hoveredBgSprite = "LineDetailButtonHovered";
            //m_DetailViewButton.focusedBgSprite = "";
            m_DetailViewButton.pressedBgSprite = "LineDetailButtonPressed";
            //m_DetailViewButton.disabledBgSprite = "";

            component.eventZOrderChanged += delegate
            {
                SetBackgroundColor();
            };

            //m_DetailViewButton.eventClick += delegate
            //{
            //    if (m_BuildingID != 0)
            //    {
            //        //Vector3 position = Singleton<NetManager>.instance.m_nodes.m_buffer[Singleton<TransportManager>.instance.m_lines.m_buffer[m_LineID].m_stops].m_position;
            //        //InstanceID instanceID = default(InstanceID);
            //        //instanceID.Building = m_LineID;
            //        //WorldInfoPanel.Show<PublicTransportWorldInfoPanel>(position, instanceID);
            //        //ToolsModifierControl.cameraController.SetTarget(instanceID, position, zoomIn: true);
            //    }
            //};

            component.eventVisibilityChanged += delegate (UIComponent comp, bool isVisible)
            {
                if (isVisible)
                {
                    RefreshData();
                }
            };
        }

        public ushort buildingID
        {
            get { return m_BuildingID; }
            set { SetBuildingID(value); }
        }

        public void RefreshData()
        {

            if (m_BuildingID == 0 || !OutsideConnectionSettingsManager.instance.SettingsDict.TryGetValue(m_BuildingID, out m_CachedSettings))
                return;

            SetupTransportTypeIcon();
            m_ConnectionNameTextfield.text = m_CachedSettings.Name;
        }

        private void SetupTransportTypeIcon()
        {
            m_TransportTypeSprite.spriteName = CommonSpriteNames.SubBarPublicTransport[(int)m_CachedSettings.Type];
        }

        public void SetBackgroundColor()
        {
            Color32 backgroundColor = m_BackgroundColor;
            backgroundColor.a = (byte)((component.zOrder % 2 != 0) ? 127 : 255);
            if (m_IsMouseOver)
            {
                backgroundColor.r = (byte)Mathf.Min(255, backgroundColor.r * 3 >> 1);
                backgroundColor.g = (byte)Mathf.Min(255, backgroundColor.g * 3 >> 1);
                backgroundColor.b = (byte)Mathf.Min(255, backgroundColor.b * 3 >> 1);
            }
            m_Background.color = backgroundColor;
        }

        private void SetBuildingID(ushort id)
        {
            m_BuildingID = id;

            RefreshData();
        }

        private void OnMouseEnter(UIComponent comp, UIMouseEventParameter param)
        {
            if (!m_IsMouseOver)
            {
                m_IsMouseOver = true;
                SetBackgroundColor();
            }
        }

        private void OnMouseLeave(UIComponent comp, UIMouseEventParameter param)
        {
            if (m_IsMouseOver)
            {
                m_IsMouseOver = false;
                SetBackgroundColor();
            }
        }
    }
}
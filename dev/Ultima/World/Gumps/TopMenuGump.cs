﻿/***************************************************************************
 *   TopMenu.cs
 *   
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/
#region usings
using UltimaXNA.Ultima.Entities.Items.Containers;
using UltimaXNA.Ultima.Entities.Mobiles;
using UltimaXNA.Ultima.UI;
using UltimaXNA.Ultima.UI.Controls;
#endregion

namespace UltimaXNA.Ultima.World.Gumps
{
    class TopMenuGump : Gump
    {
        UserInterfaceService m_UserInterface;
        WorldModel m_World;

        public TopMenuGump(Serial serial)
            : base(serial, 0)
        {
            // maximized view
            AddControl(new ResizePic(this, 1, 0, 0, 9200, 610, 27));
            AddControl(new Button(this, 1, 5, 3, 5540, 5542, 0, 2, 0));
            ((Button)LastControl).GumpOverID = 5541;
            // buttons are 2443 small, 2445 big
            // 30, 93, 201, 309, 417, 480, 543
            // map, paperdollB, inventoryB, journalB, chat, help, < ? >
            AddControl(new Button(this, 1, 30, 3, 2443, 2443, ButtonTypes.Activate, 0, (int)Buttons.Map));
            ((Button)LastControl).Caption = "<basefont color=#000000>Map";
            AddControl(new Button(this, 1, 93, 3, 2445, 2445, ButtonTypes.Activate, 0, (int)Buttons.Paperdoll));
            ((Button)LastControl).Caption = "<basefont color=#000000>Paperdoll";
            AddControl(new Button(this, 1, 201, 3, 2445, 2445, ButtonTypes.Activate, 0, (int)Buttons.Inventory));
            ((Button)LastControl).Caption = "<basefont color=#000000>Inventory";
            AddControl(new Button(this, 1, 309, 3, 2445, 2445, ButtonTypes.Activate, 0, (int)Buttons.Journal));
            ((Button)LastControl).Caption = "<basefont color=#000000>Journal";
            AddControl(new Button(this, 1, 417, 3, 2443, 2443, ButtonTypes.Activate, 0, (int)Buttons.Chat));
            ((Button)LastControl).Caption = "<basefont color=#000000>Chat";
            AddControl(new Button(this, 1, 480, 3, 2443, 2443, ButtonTypes.Activate, 0, (int)Buttons.Help));
            ((Button)LastControl).Caption = "<basefont color=#000000>Help";
            AddControl(new Button(this, 1, 543, 3, 2443, 2443, ButtonTypes.Activate, 0, (int)Buttons.Question));
            ((Button)LastControl).Caption = "<basefont color=#000000>Debug";
            // minimized view
            AddControl(new ResizePic(this, 2, 0, 0, 9200, 30, 27));
            AddControl(new Button(this, 2, 5, 3, 5537, 5539, 0, 1, 0));
            ((Button)LastControl).GumpOverID = 5538;

            m_UserInterface = ServiceRegistry.GetService<UserInterfaceService>();
            m_World = ServiceRegistry.GetService<WorldModel>();

            IsUncloseableWithRMB = true;

            Layer = GumpLayer.Over;
        }

        public override void ActivateByButton(int buttonID)
        {
            switch ((Buttons)buttonID)
            {
                case Buttons.Map:
                    if (m_UserInterface.GetControl<MiniMapGump>() == null)
                        m_UserInterface.AddControl(new MiniMapGump(), 566, 25);
                    else
                        m_UserInterface.RemoveControl<MiniMapGump>();
                    break;
                case Buttons.Paperdoll:
                    Mobile player = (Mobile)EntityManager.GetPlayerObject();
                    if (m_UserInterface.GetControl<PaperDollGump>(player.Serial) == null)
                        m_UserInterface.AddControl(new PaperDollGump((Mobile)EntityManager.GetPlayerObject()), 400, 100);
                    else
                        m_UserInterface.RemoveControl<PaperDollGump>(player.Serial);
                    break;
                case Buttons.Inventory:
                    // opens the player's backpack.
                    PlayerMobile mobile = (PlayerMobile)EntityManager.GetPlayerObject();
                    Container backpack = mobile.Backpack;
                    m_World.Interaction.DoubleClick(backpack);
                    break;
                case Buttons.Journal:
                    if (m_UserInterface.GetControl<JournalGump>() == null)
                        m_UserInterface.AddControl(new JournalGump(), 80, 80);
                    else
                        m_UserInterface.RemoveControl<JournalGump>();
                    break;
                case Buttons.Chat:
                    break;
                case Buttons.Help:
                    break;
                case Buttons.Question:
                    if (m_UserInterface.GetControl<DebugGump>() == null)
                        m_UserInterface.AddControl(new DebugGump(), 50, 50);
                    else
                        m_UserInterface.RemoveControl<DebugGump>();
                    break;
            }
        }

        enum Buttons
        {
            Map,
            Paperdoll,
            Inventory,
            Journal,
            Chat,
            Help,
            Question
        }
    }
}

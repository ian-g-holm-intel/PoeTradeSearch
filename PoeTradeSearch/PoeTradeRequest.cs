using System.Collections.Generic;

namespace PoeTradeSearch
{
    public class PoeTradeRequest
    {
        public string League { get; set; } = "";
        public string XType { get; set; } = "";
        public string XBase { get; set; } = "";
        public string Name { get; set; } = "";
        public string Dmg_Min { get; set; } = "";
        public string Dmg_Max { get; set; } = "";
        public string APS_Min { get; set; } = "";
        public string APS_Max { get; set; } = "";
        public string Crit_Min { get; set; } = "";
        public string Crit_Max { get; set; } = "";
        public string DPS_Min { get; set; } = "";
        public string DPS_Max { get; set; } = "";
        public string EDPS_Min { get; set; } = "";
        public string EDPS_Max { get; set; } = "";
        public string PDPS_Min { get; set; } = "";
        public string PDPS_Max { get; set; } = "";
        public string Armour_Min { get; set; } = "";
        public string Armour_Max { get; set; } = "";
        public string Evasion_Min { get; set; } = "";
        public string Evasion_Max { get; set; } = "";
        public string Shield_Min { get; set; } = "";
        public string Shield_Max { get; set; } = "";
        public string Block_Min { get; set; } = "";
        public string Block_Max { get; set; } = "";
        public string Sockets_Min { get; set; } = "";
        public string Sockets_Max { get; set; } = "";
        public string Link_Min { get; set; } = "";
        public string Link_Max { get; set; } = "";
        public string Sockets_R { get; set; } = "";
        public string Sockets_G { get; set; } = "";
        public string Sockets_B { get; set; } = "";
        public string Sockets_W { get; set; } = "";
        public string Linked_R { get; set; } = "";
        public string Linked_G { get; set; } = "";
        public string Linked_B { get; set; } = "";
        public string Linked_W { get; set; } = "";
        public string RLevel_Min { get; set; } = "";
        public string RLevel_Max { get; set; } = "";
        public string RStr_Min { get; set; } = "";
        public string RStr_Max { get; set; } = "";
        public string RDex_Min { get; set; } = "";
        public string RDex_Max { get; set; } = "";
        public string RInt_Min { get; set; } = "";
        public string RInt_Max { get; set; } = "";
        public string Q_Min { get; set; } = "";
        public string Q_Max { get; set; } = "";
        public string Level_Min { get; set; } = "";
        public string Level_Max { get; set; } = "";
        public string ILvl_Min { get; set; } = "";
        public string ILvl_Max { get; set; } = "";
        public string Rarity { get; set; } = "";
        public string Seller { get; set; } = "";
        public string XThread { get; set; } = "";
        public string Identified { get; set; } = "";
        public int Corrupted { get; set; } = 0;
        public string Online { get; set; } = "x";
        public string Buyout { get; set; } = "";
        public string Altart { get; set; } = "";
        public string CapQuality { get; set; } = "x";
        public string Buyout_Min { get; set; } = "";
        public string Buyout_Max { get; set; } = "";
        public string Buyout_Currency { get; set; } = "";
        public string Crafted { get; set; } = "";
        public string Enchanted { get; set; } = "";
        public ParamModGroup ModGroup = new ParamModGroup();

        public string ToPayload()
        {
            var modGroupStr = ModGroup.ToPayload();
            return
                $"league={League}&type={XType}&base={XBase}&name={Name}&dmg_min={Dmg_Min}&dmg_max={Dmg_Max}&aps_min={APS_Min}&aps_max={APS_Max}&crit_min={Crit_Min}&crit_max={Crit_Max}&dps_min={DPS_Min}&dps_max={DPS_Max}&edps_min={EDPS_Min}&edps_max={EDPS_Max}&pdps_min={PDPS_Min}&pdps_max={PDPS_Max}&armour_min=" +
                $"{Armour_Min}&armour_max={Armour_Max}&evasion_min={Evasion_Min}&evasion_max={Evasion_Max}&shield_min={Shield_Min}&shield_max={Shield_Max}&block_min={Block_Min}&block_max={Block_Max}&sockets_min={Sockets_Min}&sockets_max={Sockets_Max}&link_min={Link_Min}&link_max={Link_Max}&sockets_r={Sockets_R}&sockets_g=" +
                $"{Sockets_G}&sockets_b={Sockets_B}&sockets_w={Sockets_W}&linked_r={Linked_R}&linked_g={Linked_G}&linked_b={Linked_B}&linked_w={Linked_W}&rlevel_min={RLevel_Min}&rlevel_max={RLevel_Max}&rstr_min={RStr_Min}&rstr_max={RStr_Max}&rdex_min={RDex_Min}&rdex_max={RDex_Max}&rint_min={RInt_Min}&rint_max=" +
                $"{RInt_Max}{modGroupStr}&q_min={Q_Min}&q_max={Q_Max}&level_min={Level_Min}&level_max={Level_Max}&ilvl_min={ILvl_Min}&ilvl_max={ILvl_Max}&rarity={Rarity}&seller={Seller}&thread={XThread}&identified={Identified}&corrupted={Corrupted}&online={Online}&has_buyout={Buyout}&altart={Altart}&capquality=" +
                $"{CapQuality}&buyout_min={Buyout_Min}&buyout_max={Buyout_Max}&buyout_currency={Buyout_Currency}&crafted={Crafted}&enchanted={Enchanted}";
        }
    }

    public class ParamModGroup
    {
        private readonly List<ParamMod> modArray = new List<ParamMod>();

        public string Group_Type { get; set; } = "And";
        public string Group_Min { get; set; } = "";
        public string Group_Max { get; set; } = "";
        public int Group_Count { get; set; } = 1;

        public void AddMod(ParamMod mod)
        {
            modArray.Add(mod);
        }

        public string ToPayload()
        {
            if (modArray.Count == 0)
                AddMod(new ParamMod());

            var output = "";
            Group_Count = modArray.Count;
            foreach (var mod in modArray)
            {
                output += mod.ToPayload();
            }
            return output + $"&group_type={Group_Type}&group_min={Group_Min}&group_max={Group_Max}&group_count={Group_Count}";
        }
    }

    public class ParamMod
    {
        public string Mod_Name { get; set; } = "";
        public string Mod_Min { get; set; } = "";
        public string Mod_Max { get; set; } = "";

        public string ToPayload()
        {
            Mod_Name = Mod_Name.Replace("+", "%2B");
            return $"&mod_name={Mod_Name}&mod_min={Mod_Min}&mod_max={Mod_Max}";
        }
    }
}

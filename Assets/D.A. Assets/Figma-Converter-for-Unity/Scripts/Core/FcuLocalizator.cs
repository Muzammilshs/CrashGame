﻿using DA_Assets.Shared;

using UnityEngine;

namespace DA_Assets.FCU
{
    [CreateAssetMenu(menuName = DAConstants.Publisher + "/FcuLocalizator")]
    public class FcuLocalizator : DALocalizator<FcuLocalizator> { }

    public static class FcuLocExtensions
    {
        public static string Localize(this FcuLocKey key, params object[] args) =>
            FcuLocalizator.Instance.Localize(key.ToString(), Language.en, args);
    }

    public enum FcuLocKey
    {
        log_need_auth,
        log_incorrent_project_url,
        log_project_downloaded,
        log_frames_finded,

        log_auth_complete,

        log_open_auth_page,
        log_downloading_images,
        log_getting_links,
        log_links_added,

        log_frames_not_found,
        log_import_complete,
    
        log_current_canvas_metas_destroy,
        log_current_canvas_childs_destroy,

        log_project_not_found,

        log_cant_auth,
        log_cant_get_profile_info,
        log_unknown_error,
        log_ssl_error,

        tooltip_open_settings_window,

        tooltip_import_frames,

        label_made_by,

        label_user_name,
        tooltip_user_id,
        tooltip_asset_instance_id,
        label_kilobytes,
        label_no_recent_projects,
        label_main_settings,
        label_token,
        tooltip_token,
        label_auth,
        tooltip_auth,
        label_project_url,
        tooltip_project_url,
        tooltip_recent_projects,
        label_images_format,
        tooltip_images_format,
        label_images_scale,
        tooltip_images_scale,
        label_image_component,
        tooltip_image_component,
        label_text_component,
        tooltip_text_component,
        label_shadow_type,
        tooltip_shadow_type,

        label_use_i2localization,
        tooltip_use_i2localization,
        label_redownload_sprites,
        tooltip_redownload_sprites,
        label_debug_mode,
        tooltip_debug_mode,
        label_pui_settings,
        label_image_type,

        label_pui_falloff_distance,

        label_unity_text_settings,

        label_best_fit,
        tooltip_best_fit,
        label_line_spacing,
        tooltip_line_spacing,
        label_horizontal_overflow,
        tooltip_horizontal_overflow,
        label_vertical_overflow,
        tooltip_vertical_overflow,
        label_textmeshpro_settings,
        label_auto_size,
        tooltip_auto_size,
        label_override_tags,
        tooltip_override_tags,
        label_wrapping,
        tooltip_wrapping,
        label_rich_text,
        tooltip_rich_text,
        label_raycast_target,
        tooltip_raycast_target,
        label_parse_escape_characters,
        tooltip_parse_escape_characters,
        label_visible_descender,
        tooltip_visible_descender,
        label_kerning,
        tooltip_kerning,
        label_extra_padding,
        tooltip_extra_padding,
        label_overflow,
        tooltip_overflow,
        label_horizontal_mapping,
        tooltip_horizontal_mapping,
        label_vertical_mapping,
        tooltip_vertical_mapping,
        label_geometry_sorting,
        tooltip_geometry_sorting,
        label_dependencies,

        label_frames_to_import,

        label_debug_tools,
        tooltip_debug_tools,

        label_auto_disable_compress_assets_on_import,
        tooltip_auto_disable_compress_assets_on_import,

        log_malformed_url,

        log_sprites_removed,
        label_sprites_path,
        tooltip_sprites_path,
        label_remove_unused_sprites,
        label_remove,

        label_dont_remove_fcu_meta,
        label_more_about_layout_updating,

        log_tagging,
        log_instantiate_game_objects,

        log_start_download_images,
        log_start_setting_transform,
        log_drawn_count,
        log_mark_as_sprite,

        label_ttf_path,

        label_change,
        label_select_fonts_folder,
        label_select_folder,

        label_add_ttf_fonts_from_folder,
        label_add_tmp_fonts_from_folder,
        tooltip_add_fonts_from_folder,
        log_added_total,
        log_start_adding_to_fonts_list,

        label_mpuikit_settings,
        label_unity_image_settings,
        label_shapes2d_settings,

        log_api_waiting,

        tooltip_stop_import,
        label_import_stoped_manually,

        log_unknown_aligment,

        label_button_type,
        tooltip_button_type,
        label_components_settings,

        label_asset,
        log_cache_restored,
        label_import_events,
        log_generating_sprites,
        fonts_settings,
        log_downloading_fonts,

        label_sampling_point_size,
        label_atlas_padding,
        label_render_mode,
        label_atlas_population_mode,
        label_enable_multi_atlas_support,
        tooltip_sampling_point_size,
        tooltip_atlas_padding,
        tooltip_render_mode,
        tooltip_atlas_population_mode,
        tooltip_enable_multi_atlas_support,
        label_atlas_resolution,
        tooltip_atlas_resolution,
        label_font_subset,
        tooltip_font_subset,
        label_prefabs,
        label_prefab_settings,
        label_text_prefab_naming_mode,
        label_prefabs_path,
        label_select_prefabs_folder,
        log_search_local_prefabs,
        log_local_prefabs_found,
        label_raw_import,
        tooltip_raw_import,
        log_loading_from_cache,
        log_gameobject_not_selected_in_hierarchy,
        log_component_not_selected_in_hierarchy,
        cant_load_sprite,
        cant_download_sprite, 
        tooltip_change_window_mode,
        tooltip_download_project,
        loading_google_fonts,
        label_download_fonts_from_project,
        cant_download_fonts,
        log_generating_tmp_fonts,
        label_google_fonts_api_key,
        tooltip_google_fonts_api_key,
        label_preserve_aspect,
        label_maskable,
        label_raycast_padding,
        log_fcu_assigned,
        tooltip_open_fcu_window,
        label_log_default,
        label_log_set_tag,
        label_log_downloadable,
        label_log_transform,
        label_log_go_drawer,
        log_dev_function_enabled,
        label_import_components,
        tooltip_import_components,
        label_asset_creator_settings,
        tooltip_asset_creator_settings,
        label_get_google_api_key,
        label_tmp_path,
        label_google_fonts_settings,
        label_font_settings,
        tooltip_prefabs_path,
        label_humanized_color,
        label_hex_color,
        label_figma_color,
        log_start_creating_prefabs,
        log_prefabs_created,
        log_incorrect_selection,
        log_no_sync_helper,
        label_copy_to_clipboard,
        label_open_diff_checker,
        label_comparer_desc,
        label_object_comparer,
        label_debug,
        label_asset_dependencies,
        log_scene_backup_created,
        log_cant_get_image_links,
        log_cant_draw_object,
        log_no_components_in_gameobject,
        log_props_reset,
        log_object_reset,
        log_project_empty,
        label_beta_version,
        label_rateme_desc,
        label_rate,
        label_rateme,
        label_settings,
        label_fcu,
        cant_generate_fonts,
        log_closed_after_script_reload,
        label_unity_comp,
        label_figma_comp,
        tooltip_unity_comp,
        tooltip_figma_comp,
        label_figma_comp_desc,
        log_not_authorized,
        log_corrent_project_url,
        log_cant_get_images,
        log_asset_not_imported,
        label_supported_from_unity_version,
        log_scene_backup_creation_error,
        log_no_google_fonts_api_key,
        label_go_layer,
        tooltip_go_layer,
        log_enable_http_project_settings,
        tooltip_recent_tokens,
        label_no_recent_sessions,
        label_ui_framework,
        tooltip_ui_framework,
        label_uitk_output_path,
        tooltip_uitk_output_path,
        log_cant_find_package,
        log_package_installed,
        log_getting_frames,
        log_cant_get_part_of_frames,
        label_positioning_mode,
        tooltip_positioning_mode,
        label_dabutton_settings,
        label_button_settings,
        label_fade_duration,
        label_old_data,
        label_copy_new_data,
        label_has_differences,
        label_different_component_data,
        label_copy_old_data,
        label_selected_shader,
        label_shader,
        label_all,
        label_new,
        label_changed_in_figma,
        label_changed_in_unity,
        label_import,
        label_remove_from_scene,
        label_apply_and_continue,
        label_missings_in_frame,
        label_components_to_import,
        tooltip_apply_and_continue,
        tooltip_remove_from_scene,
        tooltip_new,
        tooltip_new_components,
        tooltip_changed_in_figma,
        tooltip_changed_in_unity,
        tooltip_components_to_import,
        tooltip_all_components,
        log_nothing_to_import,
        label_components_with_count,
        label_other,
        tooltip_other_components,
        label_without_changes,
        tooltip_label_without_changes,
        label_pivot_type,
        tooltip_pivot_type,
        log_cant_enable_autosize_with_overflow,
        label_flip_x,
        label_flip_y,
        label_mask_interaction,
        label_sort_point,
        label_sorting_layer,
        label_sr_settings,
        label_next_order_step,
        label_pixels_per_unit,
        tooltip_pixels_per_unit,
        label_find_added_objects,
        label_farsi,
        tooltip_farsi,
        label_force_fix,
        tooltip_force_fix,
        label_preserve_numbers,
        tooltip_preserve_numbers,
        label_fix_tags,
        tooltip_fix_tags,
        label_procedural_ui_settings,
        label_raw_image_settings,
        label_log_component_drawer,
        label_log_hash_generator_drawer,
        tooltip_uitk_linking_mode,
        label_uitk_linking_mode,
        label_script_generator,
        label_enabled,
        label_namespace,
        label_scripts_output_path,
        tooltip_script_generator,
        label_fcu_is_null,
        label_preserve_ratio_mode,
        tooltip_preserve_ratio_mode,
        log_name_linking_not_recommended
    }
}
